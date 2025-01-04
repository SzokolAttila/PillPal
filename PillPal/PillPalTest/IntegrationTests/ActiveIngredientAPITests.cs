using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.MedicineActiveIngredientDTOs;
using PillPalLib.DTOs.ActiveIngredientDTOs;
using PillPalLib.DTOs.UserDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class ActiveIngredientAPITests
    {
        private MedicineAPIHandler medicineHandler;
        private UserAPIHandler userHandler;
        private ActiveIngredientAPIHandler activeIngredientHandler;
        private MedicineActiveIngredientAPIHandler medicineActiveIngredientHandler;
        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            medicineHandler = new(client: api.CreateClient());
            userHandler = new(client: api.CreateClient());
            medicineActiveIngredientHandler = new(client: api.CreateClient());
            activeIngredientHandler = new(client: api.CreateClient());
        }
        [TestMethod ]
        public void AdminRoleNeededToCreateActiveIngredient()
        {
            var userToken = GetUserToken();
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine" };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => activeIngredientHandler.CreateActiveIngredient(activeIngredient, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken);
            Assert.AreEqual(1, activeIngredientHandler.GetAll().Count());
            var medicineActiveIngredient = new CreateMedicineActiveIngredientDto() { MedicineId = 1, ActiveIngredientId = 1 };
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, adminToken);
            Assert.AreEqual("Caffeine", medicineHandler.GetMedicine(1).ActiveIngredients.ElementAt(0));
        }
        [TestMethod]
        public void ActiveIngredientLengthNeedsToBeAtLeast3()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "ue" };
            var exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken));
            Assert.AreEqual("Ingredient name is too short.", exception.Message);
        }
        [TestMethod]
        public void ActiveIngredientNeedsToBeUnique()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine" };
            activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken));
            Assert.AreEqual("Active ingredient already added.", exception.Message);
        }
        [TestMethod]
        public void CannotAddActiveIngredientToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine" };
            activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken);
            var medicineActiveIngredient = new CreateMedicineActiveIngredientDto() { MedicineId = 1, ActiveIngredientId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotAddNonExistantActiveIngredientToMedicine()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var medicineActiveIngredient = new CreateMedicineActiveIngredientDto() { MedicineId = 1, ActiveIngredientId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, adminToken));
            Assert.AreEqual("ActiveIngredient with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void AdminRoleNeededToEditActiveIngredient()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine" };
            activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken);
            var medicineActiveIngredient = new CreateMedicineActiveIngredientDto() { MedicineId = 1, ActiveIngredientId = 1 };
            medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, adminToken);
            activeIngredient.Ingredient = "diclofenac";
            var exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.EditActiveIngredient(1, activeIngredient, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            activeIngredientHandler.EditActiveIngredient(1, activeIngredient, adminToken);
            Assert.AreEqual("diclofenac", medicineHandler.GetMedicine(1).ActiveIngredients.ElementAt(0));
        }
        [TestMethod]
        public void CannotEditNonExistantActiveIngredient()
        {
            var adminToken = GetAdminToken();
            var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine " };
            var exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.EditActiveIngredient(1, activeIngredient, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        //[TestMethod]
        //public void AdminRoleNeededToDeleteActiveIngredient()
        //{
        //    var adminToken = GetAdminToken();
        //    CreateMedicine(adminToken);
        //    var userToken = GetUserToken();
        //    var activeIngredient = new CreateActiveIngredientDto() { Ingredient = "Caffeine" };
        //    activeIngredientHandler.CreateActiveIngredient(activeIngredient, adminToken);
        //    var medicineActiveIngredient = new CreateMedicineActiveIngredientDto() { MedicineId = 1, ActiveIngredientId = 1 };
        //    medicineActiveIngredientHandler.CreateMedicineActiveIngredient(medicineActiveIngredient, adminToken);
        //    var exception = Assert.ThrowsException<ArgumentException>(
        //        () => medicineActiveIngredientHandler.DeleteMedicineActiveIngredient(1, userToken));
        //    Assert.AreEqual("Forbidden", exception.Message);
        //    medicineActiveIngredientHandler.DeleteMedicineActiveIngredient(1, adminToken);
        //    Assert.AreEqual(0, medicineHandler.GetMedicine(1).ActiveIngredients.Count());
        //    exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.DeleteActiveIngredient(1, userToken));
        //    Assert.AreEqual("Forbidden", exception.Message);
        //    activeIngredientHandler.DeleteActiveIngredient(1, adminToken);
        //    Assert.AreEqual(0, activeIngredientHandler.GetAll().Count());
        //}
        [TestMethod]
        public void CannotDeleteNonExistantActiveIngredient()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => activeIngredientHandler.DeleteActiveIngredient(1, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
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
