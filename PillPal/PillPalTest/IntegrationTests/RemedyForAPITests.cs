using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.MedicineRemedyForDTOs;
using PillPalLib.DTOs.PackageUnitDTOs;
using PillPalLib.DTOs.RemedyForDTOs;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class RemedyForAPITests
    {
        private MedicineAPIHandler medicineHandler;
        private UserAPIHandler userHandler;
        private RemedyForAPIHandler remedyForHandler;
        private PackageUnitAPIHandler packageUnitHandler;
        private MedicineRemedyForAPIHandler medicineRemedyForHandler;
        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            medicineHandler = new(client: api.CreateClient());
            packageUnitHandler = new(client: api.CreateClient());
            userHandler = new(client: api.CreateClient());
            medicineRemedyForHandler = new(client: api.CreateClient());
            remedyForHandler = new(client: api.CreateClient());
        }
        [TestMethod]
        public void AdminRoleNeededToCreateRemedyFor()
        {
            var userToken = GetUserToken();
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var remedyFor = new CreateRemedyForDto() { Ailment = "Tummyache" };
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.CreateRemedyFor(remedyFor, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            Assert.AreEqual(1, remedyForHandler.GetAll().Count());
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { MedicineId = 1, RemedyForId = 1 };
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, adminToken);
            Assert.AreEqual("Tummyache", medicineHandler.GetMedicine(1).RemedyForAilments.ElementAt(0));
        }
        [TestMethod]
        public void RemedyForLengthNeedsToBeAtLeast3()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var remedyFor = new CreateRemedyForDto() { Ailment = "ue" };
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.CreateRemedyFor(remedyFor, adminToken));
            Assert.AreEqual("Ailment name is too short.", exception.Message);
        }
        [TestMethod]
        public void RemedyForNeedsToBeUnique()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var remedyFor = new CreateRemedyForDto() { Ailment = "tummyache" };
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.CreateRemedyFor(remedyFor, adminToken));
            Assert.AreEqual("Ailment already added", exception.Message);
        }
        [TestMethod]
        public void CannotAddRemedyForToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var remedyFor = new CreateRemedyForDto() { Ailment = "tummyache" };
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { MedicineId = 1, RemedyForId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotAddNonExistantRemedyForToMedicine()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { MedicineId = 1, RemedyForId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, adminToken));
            Assert.AreEqual("RemedyFor with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void AdminRoleNeededToEditRemedyFor()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var remedyFor = new CreateRemedyForDto() { Ailment = "tummyache" };
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { MedicineId = 1, RemedyForId = 1 };
            medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, adminToken);
            remedyFor.Ailment = "headache";
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.EditRemedyFor(1, remedyFor, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            remedyForHandler.EditRemedyFor(1, remedyFor, adminToken);
            Assert.AreEqual("headache", medicineHandler.GetMedicine(1).RemedyForAilments.ElementAt(0));
            Assert.AreEqual("headache", remedyForHandler.Get(1).Ailment);
            remedyFor.Ailment = "tummyache";
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            medicineRemedyFor.RemedyForId = 2;
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineRemedyForHandler.EditMedicineRemedyFor(1, medicineRemedyFor, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineRemedyForHandler.EditMedicineRemedyFor(1, medicineRemedyFor, adminToken);
            Assert.AreEqual("tummyache", medicineHandler.GetMedicine(1).RemedyForAilments.First());
            Assert.AreEqual(2, medicineRemedyForHandler.Get(1).First().RemedyForId);
        }
        [TestMethod]
        public void CannotEditNonExistantRemedyFor()
        {
            var adminToken = GetAdminToken();
            var remedyFor = new CreateRemedyForDto() { Ailment = "tummyache " };
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.EditRemedyFor(1, remedyFor, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { RemedyForId = 1, MedicineId = 1 };
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineRemedyForHandler.EditMedicineRemedyFor(1, medicineRemedyFor, adminToken));
            Assert.AreEqual(exception.Message, "Not Found");
        }
        [TestMethod]
        public void AdminRoleNeededToDeleteRemedyFor()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();

            var remedyFor = new CreateRemedyForDto() { Ailment = "tummyache" };
            remedyForHandler.CreateRemedyFor(remedyFor, adminToken);
            var medicineRemedyFor = new CreateMedicineRemedyForDto() { MedicineId = 1, RemedyForId = 1 };
            medicineRemedyForHandler.CreateMedicineRemedyFor(medicineRemedyFor, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => medicineRemedyForHandler.DeleteMedicineRemedyFor(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineRemedyForHandler.DeleteMedicineRemedyFor(1, adminToken);
            Assert.AreEqual(0, medicineHandler.GetMedicine(1).RemedyForAilments.Count());
            exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.DeleteRemedyFor(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            remedyForHandler.DeleteRemedyFor(1, adminToken);
            Assert.AreEqual(0, remedyForHandler.GetAll().Count());
            Assert.AreEqual(0, medicineRemedyForHandler.GetAll().Count());
        }
        [TestMethod]
        public void CannotDeleteNonExistantRemedyFor()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => remedyForHandler.DeleteRemedyFor(1, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
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
