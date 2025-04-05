using Microsoft.AspNetCore.Components.Forms;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.MedicineSideEffectDTOs;
using PillPalLib.DTOs.PackageUnitDTOs;
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
        private PackageUnitAPIHandler packageUnitHandler;
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
            packageUnitHandler = new (client: api.CreateClient());
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
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            Assert.AreEqual(1, sideEffectHandler.GetAll().Count());
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1};
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
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
            Assert.AreEqual("A mellékhatás neve túl rövid.", exception.Message);
        }
        [TestMethod]
        public void SideEffectNeedsToBeUnique()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var sideEffect = new CreateSideEffectDto() { Effect = "tummyache" };
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.CreateSideEffect(sideEffect, adminToken));
            Assert.AreEqual("A mellékhatás már létezik.", exception.Message);
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
            Assert.AreEqual("Nem létezik gyógyszer a megadott ID-val.", exception.Message);
        }
        [TestMethod]
        public void CannotAddNonExistantSideEffectToMedicine()
        {
            var adminToken = GetAdminToken();
            CreateMedicine(adminToken);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId = 1 };
            var exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.CreateMedicineSideEffect(medicineSideEffect, adminToken));
            Assert.AreEqual("Nem létezik mellékhatás a megadott ID-val.", exception.Message);
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
            Assert.AreEqual("Nem létezik gyógyszer a megadott ID-val.", exception.Message);
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
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
            sideEffectHandler.EditSideEffect(1, sideEffect, adminToken);
            Assert.AreEqual("headache", medicineHandler.GetMedicine(1).SideEffects.ElementAt(0));
            Assert.AreEqual("headache", sideEffectHandler.Get(1).Effect);
            sideEffect.Effect = "tummyache";
            sideEffectHandler.CreateSideEffect(sideEffect, adminToken);
            medicineSideEffect.SideEffectId = 2;
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
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
            Assert.AreEqual("Nem található.", exception.Message);
            var medicineSideEffect = new CreateMedicineSideEffectDto() { MedicineId = 1, SideEffectId= 1 };
            exception = Assert.ThrowsException<ArgumentException>(
                () => medicineSideEffectHandler.EditMedicineSideEffect(1, medicineSideEffect, adminToken));
            Assert.AreEqual("Nem található.", exception.Message);
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
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
            medicineSideEffectHandler.DeleteMedicineSideEffect(1, adminToken);
            Assert.AreEqual(0, medicineHandler.GetMedicine(1).SideEffects.Count());
            exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.DeleteSideEffect(1, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", exception.Message);
            sideEffectHandler.DeleteSideEffect(1, adminToken);
            Assert.AreEqual(0, sideEffectHandler.GetAll().Count());
            Assert.AreEqual(0, medicineSideEffectHandler.GetAll().Count());
        }
        [TestMethod]
        public void CannotDeleteNonExistantSideEffect()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => sideEffectHandler.DeleteSideEffect(1, adminToken));
            Assert.AreEqual("Nem található.", exception.Message);
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
