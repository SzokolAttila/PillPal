using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.DTOs.PackageSizeDTOs;
using PillPalLib.DTOs.PackageUnitDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class PackageSizeAPITests
    {
        private MedicineAPIHandler medicineHandler;
        private UserAPIHandler userHandler;
        private PackageSizeAPIHandler packageSizeHandler;
        private PackageUnitAPIHandler packageUnitHandler;
        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            userHandler  = new (client: api.CreateClient());
            medicineHandler  = new (client: api.CreateClient());
            packageSizeHandler = new (client: api.CreateClient());
            packageUnitHandler = new (client: api.CreateClient());
        }
        [TestMethod]
        public void AdminRoleNeededToCreatePackageSize()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
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
            Assert.AreEqual("A kiszerelésnek nullánál nagyobbnak kell lennie.", exception.Message);
        }
        [TestMethod]
        public void CannotAddPackageSizeToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId = 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.CreatePackageSize(packageSize, adminToken));
            Assert.AreEqual("Nem létezik gyógyszer a megadott ID-val.", exception.Message);
        }
        [TestMethod]
        public void CannotEditNonExistantPackageSize()
        {
            var adminToken = GetAdminToken();
            var packageSize = new CreatePackageSizeDto() { MedicineId= 1, Size = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.EditPackageSize(1, packageSize, adminToken));
            Assert.AreEqual("Nem található.", exception.Message);
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
            Assert.AreEqual("A kiszerelésnek nullánál nagyobbnak kell lennie.", exception.Message);
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
            Assert.AreEqual("Ez a kiszerelés már hozzá lett adva ehhez a gyógyszerhez.", exception.Message);    
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
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
            packageSizeHandler.EditPackageSize(1, packageSize, adminToken);
            Assert.AreEqual(30, medicineHandler.GetMedicine(1).PackageSizes.ElementAt(0));
            Assert.AreEqual(30, packageSizeHandler.Get(1).First().Size);
        }
        [TestMethod]
        public void CannotDeleteNonExistantPackageSize()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => packageSizeHandler.DeletePackageSize(1, adminToken));
            Assert.AreEqual("Nem található.", exception.Message);
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
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
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
            Assert.AreEqual("Ez a kiszerelés már hozzá lett adva ehhez a gyógyszerhez.", exception.Message);
        }
        private string GetAdminToken()
        {
            CreateUserDto admin = new() { UserName = "administrator", Password = "aA1?aA1?" };
            userHandler.CreateUser(admin);
            return userHandler.Login(admin).Token;
        }
        private string GetUserToken()
        {
            CreateUserDto user = new() { UserName = "username", Password = "aA1?aA1?" };
            userHandler.CreateUser(user);   
            return userHandler.Login(user).Token;
        }
        private void CreatePackageUnit(string token)
        {
            var packageUnit = new CreatePackageUnitDto() { Name = "packageUnit" };
            packageUnitHandler.CreatePackageUnit(packageUnit, token);
        }
        private void CreateMedicine(string token)
        {
            CreatePackageUnit(token);
            CreateMedicineDto medicine = new()
            {
                Name = "gyogyszer1",
                Description = "ez egy gyógyszer",
                Manufacturer = "a gyógyszergyártója",
                PackageUnitId = 1,
            };
            medicineHandler.CreateMedicine(medicine, token);
        }
    }
}
