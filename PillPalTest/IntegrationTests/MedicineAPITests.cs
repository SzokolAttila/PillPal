using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.DTOs.PackageUnitDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class MedicineAPITests
    {
        private MedicineAPIHandler handler;
        private UserAPIHandler userHandler;
        private PackageUnitAPIHandler packageUnitHandler;

        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            handler = new (client: api.CreateClient());
            userHandler = new (client: api.CreateClient());
            packageUnitHandler = new (client: api.CreateClient());
        }

        [TestMethod]
        public void PostingAsAnonyomousOrUserThrowsArgumentException()
        {
            string userToken = GetUserToken();
            CreateMedicineDto medicine = MockMedicine();

            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, ""));
            Assert.AreEqual("Nincs bejelentkezve.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);
        }

        [TestMethod]
        public void PuttingAsAnonyomousOrUserThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            string userToken = GetUserToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, ""));
            Assert.AreEqual("Nincs bejelentkezve.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);
        }

        [TestMethod]
        public void DeletingAsAnonyomousOrUserThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            string userToken = GetUserToken();
            MockMedicine(adminToken, true);
            
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(1, ""));
            Assert.AreEqual("Nincs bejelentkezve.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(1, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);
        }

        [TestMethod]
        public void PuttingToNonExistentIdThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            var medicine = MockMedicine(adminToken, true);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(100, medicine, adminToken));
            Assert.AreEqual("Nem található.", message.Message);
        }

        [TestMethod]
        public void DeletingNonExistentIdThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(100, adminToken));
            Assert.AreEqual("Nem található.", message.Message);
        }

        [TestMethod]
        public void PostingNewMedicineIncrementsNumberOfMedicines()
        {
            string adminToken = GetAdminToken();
            for (int i = 0; i<5; i++)
            {
                MockMedicine(adminToken, true);
            }
            Assert.AreEqual(5,handler.GetMedicines().Count());
        }

        [TestMethod]
        public void DeletingMedicineLowersTheNumberOfMedicines()
        {
            string adminToken = GetAdminToken();
            for (int i = 0; i < 5; i++)
            {
                MockMedicine(adminToken, true);
            }
            handler.DeleteMedicine(2, adminToken);
            handler.DeleteMedicine(5, adminToken);
            Assert.AreEqual(3, handler.GetMedicines().Count());
        }

        [TestMethod]
        public void UpdatingTheNameChangesIt()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);
            Assert.AreNotEqual("Cataflam", handler.GetMedicine(1).Name);
            medicine.Name = "Cataflam";
            handler.UpdateMedicine(1, medicine, adminToken);
            Assert.AreEqual("Cataflam", handler.GetMedicine(1).Name);
        }

        [TestMethod]
        public void PostingOrPuttingMedicineWithShortOrLongNameThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.Name = "a";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("A gyógyszer nevének 5 és 30 karakter között kell lennie.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("A gyógyszer nevének 5 és 30 karakter között kell lennie.", message.Message);

            medicine.Name = "asdmsasvaksamclkasmclkasmclkasmclkmsclakmsclksamclakmsclaksmc";
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("A gyógyszer nevének 5 és 30 karakter között kell lennie.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("A gyógyszer nevének 5 és 30 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void PostingOrPuttingMedicineWithShortOrLongManufacturerThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.Manufacturer = "a";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("A gyártó nevének 3 és 30 karakter között kell lennie.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("A gyártó nevének 3 és 30 karakter között kell lennie.", message.Message);

            medicine.Manufacturer = "asdmsasvaksamclkasmclkasmclkasmclkmsclakmsclksamclakmsclaksmc";
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("A gyártó nevének 3 és 30 karakter között kell lennie.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("A gyártó nevének 3 és 30 karakter között kell lennie.", message.Message);
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
        private CreateMedicineDto MockMedicine(string adminToken = "", bool withPosting = false)
        {
            CreateMedicineDto medicine = new()
            {
                Name = "gyogyszer1",
                Description = "ez egy gyógyszer",
                Manufacturer = "a gyógyszergyártója",
                PackageUnitId = 1,
            };

            if (withPosting)
            {
                CreatePackageUnit(adminToken);
                handler.CreateMedicine(medicine, adminToken);
            }

            return medicine;
        }
    }
}
