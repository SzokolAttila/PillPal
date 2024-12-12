using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.DTOs.UserDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class MedicineAPITests
    {
        private MedicineAPIHandler handler;
        private UserAPIHandler userHandler;

        [TestInitialize]
        public void Init()
        {
            var api = new TestWebAppFactory<PillPalAPI.Program>();
            handler = new (client: api.CreateClient());
            userHandler = new (client: api.CreateClient());
        }

        [TestMethod]
        public void PostingAsAnonyomousOrUserThrowsArgumentException()
        {
            string userToken = GetUserToken();
            CreateMedicineDto medicine = MockMedicine();

            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, ""));
            Assert.AreEqual("Unauthorized", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, userToken));
            Assert.AreEqual("Forbidden", message.Message);
        }

        [TestMethod]
        public void PuttingAsAnonyomousOrUserThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            string userToken = GetUserToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, ""));
            Assert.AreEqual("Unauthorized", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, userToken));
            Assert.AreEqual("Forbidden", message.Message);
        }

        [TestMethod]
        public void DeletingAsAnonyomousOrUserThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            string userToken = GetUserToken();
            MockMedicine(adminToken, true);
            
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(1, ""));
            Assert.AreEqual("Unauthorized", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(1, userToken));
            Assert.AreEqual("Forbidden", message.Message);
        }

        [TestMethod]
        public void PuttingToNonExistentIdThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            var medicine = MockMedicine(adminToken, true);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(100, medicine, adminToken));
            Assert.AreEqual("Not Found", message.Message);
        }

        [TestMethod]
        public void DeletingNonExistentIdThrowsArgumentException()
        {
            string adminToken = GetAdminToken();
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteMedicine(100, adminToken));
            Assert.AreEqual("Not Found", message.Message);
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
            Assert.AreEqual("Medicine name must be between 5 and 30 characters.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("Medicine name must be between 5 and 30 characters.", message.Message);

            medicine.Name = "asdmsasvaksamclkasmclkasmclkasmclkmsclakmsclksamclakmsclaksmc";
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("Medicine name must be between 5 and 30 characters.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("Medicine name must be between 5 and 30 characters.", message.Message);
        }

        [TestMethod]
        public void PostingOrPuttingMedicineWithShortOrLongManufacturerThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.Manufacturer = "a";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("Manufacturer name must be between 5 and 30 characters.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("Manufacturer name must be between 5 and 30 characters.", message.Message);

            medicine.Manufacturer = "asdmsasvaksamclkasmclkasmclkasmclkmsclakmsclksamclakmsclaksmc";
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("Manufacturer name must be between 5 and 30 characters.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("Manufacturer name must be between 5 and 30 characters.", message.Message);
        }

        //[TestMethod]
        //public void PostingOrPuttingMedicineWithNoActiveIngredientsThrowsException()
        //{
        //    string adminToken = GetAdminToken();
        //    CreateMedicineDto medicine = MockMedicine(adminToken, true);

        //    var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
        //    Assert.AreEqual("A medicine without active ingredients is just a placebo.", message.Message);
        //    message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
        //    Assert.AreEqual("A medicine without active ingredients is just a placebo.", message.Message);
        //}

        [TestMethod]
        public void PostingOrPuttingMedicineWithNoPackageSizeThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.PackageSizes = new int[] { };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("There must be at least one package size.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("There must be at least one package size.", message.Message);
        }

        [TestMethod]
        public void PostingOrPuttingMedicineWithNoRemedyForThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.RemedyFor = new string[] { };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("If it's not remedy for anything then why does it exist?", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("If it's not remedy for anything then why does it exist?", message.Message);
        }

        [TestMethod]
        public void PostingOrPuttingMedicineWithNotMgOrMlPackageUnitThrowsException()
        {
            string adminToken = GetAdminToken();
            CreateMedicineDto medicine = MockMedicine(adminToken, true);

            medicine.PackageUnit = "gm";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateMedicine(medicine, adminToken));
            Assert.AreEqual("Package unit must be either mg or ml.", message.Message);
            message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateMedicine(1, medicine, adminToken));
            Assert.AreEqual("Package unit must be either mg or ml.", message.Message);
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
        private CreateMedicineDto MockMedicine(string adminToken = "", bool withPosting = false)
        {
            CreateMedicineDto medicine = new()
            {
                Name = "gyogyszer1",
                Description = "ez egy gyógyszer",
                Manufacturer = "a gyógyszergyártója",
                PackageSizes = new int[] { 50, 100 },
                PackageUnit = "mg",
                RemedyFor = new string[] { "megfázás" }
            };
            if(withPosting) handler.CreateMedicine(medicine, adminToken);
            return medicine;
        }
    }
}
