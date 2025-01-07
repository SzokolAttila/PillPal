using Microsoft.AspNetCore.Components.Forms;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.MedicineSideEffectDTOs;
using PillPalLib.DTOs.SideEffectDTOs;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class SideEffectAPITests
    {
        private MedicineAPIHandler medicineHandler;
        private UserAPIHandler userHandler;
        private SideEffectAPIHandler sideEffectHandler;
        private MedicineSideEffectAPIHandler medicineSideEffectHandler;
        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            medicineHandler = new (client: api.CreateClient());
            userHandler = new (client: api.CreateClient());
            sideEffectHandler = new (client: api.CreateClient());
            medicineSideEffectHandler = new (client: api.CreateClient());
        }
        [TestMethod]
        public void AdminRoleNeededToCreateSideEffect()
        {
            var userToken = GetUserToken();
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var sideEffect = new CreateSideEffectDto() {Effect = "Tummyache" };
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.CreateSideEffect(sideEffect, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            Assert.AreEqual(1, sideEffectHandler.GetAll().Count());
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1};
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken);
            Assert.AreEqual("Tummyache", medicineHandler.GetMedicine(1).SideEffects.ElementAt(0));
        }
        [TestMethod]
        public void SideEffectLengthNeedsToBeAtLeast3()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var sideEffect = new CreateSideEffectDto() { Effect = "ue" };
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.CreateSideEffect(sideEffect, adminToken));
            Assert.AreEqual("Side effect is too short", exception.Message);
        }
        [TestMethod]
        public void SideEffectNeedsToBeUnique()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.CreateSideEffect(sideEffect, adminToken));
            Assert.AreEqual("Side effect already added", exception.Message);
        }
        [TestMethod]
        public void CannotAddSideEffectToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotAddNonExistantSideEffectToMedicine()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken));
            Assert.AreEqual("SideEffect with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotEditToNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken);
            medicineSideEffect.MedicineId = 2;
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void AdminRoleNeededToEditSideEffect()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken);
            sideEffect.Effect = "headache";
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.EditSideEffect(1, sideEffect, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            sideEffectHandler.EditSideEffect(1, sideEffect, adminToken);
            Assert.AreEqual("headache", medicineHandler.GetMedicine(1).SideEffects.ElementAt(0));
            Assert.AreEqual("headache", sideEffectHandler.Get(1).Effect);
            sideEffect.Effect = "tummyache";
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            medicineSideEffect.SideEffectId = 2;
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, adminToken);
            Assert.AreEqual("tummyache", medicineHandler.GetMedicine(1).SideEffects.ElementAt(0));
            Assert.AreEqual(2, medicineSideEffectHandler.Get(1).First().SideEffectId);
        }
        [TestMethod]
        public void CannotEditNonExistantSideEffect()
        {
            var adminToken = GetAdminToken();
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache " };
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.EditSideEffect(1, sideEffect, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId= 1 };
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        [TestMethod]
        public void AdminRoleNeededToDeleteSideEffect()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var userToken = GetUserToken();
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => medicineSideEffectHandler.DeleteMedicineSideEffect(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            medicineSideEffectHandler.DeleteMedicineSideEffect(1, adminToken);
            Assert.AreEqual(0, medicineHandler.GetMedicine(1).SideEffects.Count());
            exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.DeleteSideEffect(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            sideEffectHandler.DeleteSideEffect(1, adminToken);
            Assert.AreEqual(0, sideEffectHandler.GetAll().Count());
            Assert.AreEqual(0, medicineSideEffectHandler.GetAll().Count());
        }
        [TestMethod]
        public void CannotDeleteNonExistantSideEffect()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.DeleteSideEffect(1, adminToken));
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
