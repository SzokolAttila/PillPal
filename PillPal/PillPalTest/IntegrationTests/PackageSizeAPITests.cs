using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.DTOs.PackageSizeDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class PackageSizeAPITests
    {
        private MedicineAPIHandler medicineHandler;
        private UserAPIHandler userHandler;
        private PackageSizeAPIHandler packageSizeHandler;
        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            userHandler  = new (client: api.CreateClient());
            medicineHandler  = new (client: api.CreateClient());
            packageSizeHandler = new (client: api.CreateClient());
        }
        [TestMethod]
        public void AdminRoleNeededToCreatePackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            Assert.AreEqual(1, packageSizeHandler.GetAll().Count());
        }
        [TestMethod]
        public void CannotCreatePackageSizeWithSizeLessThanOne()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 0 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, adminToken));
            Assert.AreEqual("Package size has to be greater than 0.", exception.Message);
        }
        [TestMethod]
        public void CannotAddPackageSizeToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotEditNonExistantPackageSize()
        {
            var adminToken = GetAdminToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId= 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.EditPackageSize(1, packageSize, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        [TestMethod]
        public void CannotEditToInvalidPackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            packageSize.Size = 0;
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.EditPackageSize(1, packageSize, adminToken));
            Assert.AreEqual("Package size has to be greater than 0.", exception.Message);
        }
        [TestMethod]
        public void CannotEditToDuplicatePackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            packageSize.Size = 2;
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.EditPackageSize(1, packageSize, adminToken));
            Assert.AreEqual("This PackageSize has already been added to this Medicine.", exception.Message);    
        }
        [TestMethod]
        public void AdminRoleNeededToEditPackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            packageSize.Size = 30;
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.EditPackageSize(1, packageSize, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            packageSizeHandler.EditPackageSize(1, packageSize, adminToken);
            Assert.AreEqual(30, medicineHandler.GetMedicine(1).PackageSizes.ElementAt(0));
        }
        [TestMethod]
        public void CannotDeleteNonExistantPackageSize()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.DeletePackageSize(1, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        [TestMethod]
        public void AdminRoleNeededToDeletePackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.DeletePackageSize(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            packageSizeHandler.DeletePackageSize(1, adminToken);
            Assert.AreEqual(0, packageSizeHandler.GetAll().Count());
        }
        [TestMethod]
        public void CannotAddDuplicatePackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            packageSizeHandler.CreatePackageSize(packageSize, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, adminToken));
            Assert.AreEqual("This PackageSize has already been added to this Medicine.", exception.Message);
        }
        private string GetAdminToken()
        {
            CreateUserDto admin = new() { UserName = "administrator", Password = "aA1?aA1?" };
            userHandler.CreateUser(admin);
            return userHandler.Login(admin);
        }
        private string GetUserToken()
        {
            CreateUserDto user = new() { UserName = "username", Password = "aA1?aA1?" };
            userHandler.CreateUser(user);   
            return userHandler.Login(user);
        }
        private void CreateMedicine(string token)
        {
            CreateMedicineDto medicine = new()
            {
                Name = "gyogyszer1",
                Description = "ez egy gyógyszer",
                Manufacturer = "a gyógyszergyártója",
                PackageUnit = "mg",
            };
            medicineHandler.CreateMedicine(medicine, token);
        }
    }
}
