using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.DTOs.ReminderDTOs;
using PillPalLib.DTOs.MedicineDTOs;
using System.Runtime.InteropServices;
using PillPalLib.DTOs.PackageUnitDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class ReminderAPITests
    {
        private ReminderAPIHandler handler;
        private UserAPIHandler userHandler;
        private MedicineAPIHandler medicineHandler;
        private PackageUnitAPIHandler packageUnitHandler;
        [TestInitialize]
        public void Init()
        {
            var app = new TestWebAppFactory<PillPalAPI.Program>();
            packageUnitHandler = new PackageUnitAPIHandler(client: app.CreateClient());
            handler = new ReminderAPIHandler(client: app.CreateClient());
            userHandler = new UserAPIHandler(client: app.CreateClient());
            medicineHandler = new MedicineAPIHandler(client: app.CreateClient());
        }
        [TestMethod]
        public void AdminRoleNeededToGetAllReminders()
        {
            var userToken = GetUserToken("brownie");
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.GetAll(userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            Assert.AreEqual(0, handler.GetAll(GetAdminToken()).Count());
        }
        [TestMethod]
        public void AdminCanGetUserReminders()
        {
            var adminToken = GetAdminToken();
            GetUserToken("brownie");
            Assert.AreEqual(0, handler.Get(1, adminToken).Count());
            Assert.AreEqual(0, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void UserCanGetOwnReminders()
        {
            var userToken = GetUserToken("brownie");
            Assert.AreEqual(0, handler.Get(1, userToken).Count());
        }
        [TestMethod]
        public void UserCannotGetOtherReminders()
        {
            var userToken = GetUserToken("bronwie");
            GetUserToken("jackie");
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.Get(2, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void DoseMgCannotBeNegativeOrZero()
        {
            var adminToken = GetAdminToken();
            SeedMedicine(adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                MedicineId = 1,
                UserId = 1,
                TakingMethod = "",
                When = "14:00:50"
            };
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.CreateReminder(reminder, adminToken));
            Assert.AreEqual("Cannot add medicine with negative dose", exception.Message);
        }
        [TestMethod]
        public void DoseCountCannotBeNegativeOrZero()
        {
            var adminToken = GetAdminToken();
            SeedMedicine(adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseCount = 0,
                MedicineId = 1,
                UserId = 1,
                TakingMethod = "",
                When = "14:00:50"
            };
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.CreateReminder(reminder, adminToken));
            Assert.AreEqual("Cannot add medicine with negative dose", exception.Message);
        }
        [TestMethod]
        public void CannotAddReminderToNonExistantUser()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(2, adminToken));
            Assert.AreEqual("User with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotAddReminderWithNonExistantMedicine()
        {
            var adminToken = GetAdminToken();
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(1, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void AdminCanCreateReminderForAnyone()
        {
            var adminToken = GetAdminToken();
            GetUserToken("browine");
            SeedMedicine(adminToken);
            var reminder = SeedReminder(1, adminToken);
            reminder.UserId = 2;
            handler.CreateReminder(reminder, adminToken);
            Assert.AreEqual(1, handler.Get(1, adminToken).Count());
            Assert.AreEqual(1, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void UserCannotAddReminderToOtherUsers()
        {
            var userToken = GetUserToken("brownie");
            var adminToken = GetAdminToken();
            SeedMedicine(adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(2, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void UserCanAddOwnReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            SeedReminder(2, userToken);
            Assert.AreEqual(1, handler.Get(2, userToken).Count());
        }
        [TestMethod]
        public void AdminCanEditAnyReminder()
        {
            var adminToken = GetAdminToken();
            GetUserToken("brownie");
            SeedMedicine(adminToken);
            CreateReminderDto reminder = SeedReminder(2, adminToken);
            reminder.TakingMethod = "take with water";
            handler.EditReminder(1, reminder, adminToken);
            Assert.AreEqual(500, handler.Get(2, adminToken).First().DoseMg);
            Assert.AreEqual("take with water", handler.Get(2, adminToken).First().TakingMethod);
        }
        [TestMethod]
        public void AdminCanMoveReminders()
        {
            var adminToken = GetAdminToken();
            GetUserToken("brownie");
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            Assert.AreEqual(0, handler.Get(1, adminToken).Count());
            Assert.AreEqual(1, handler.Get(2, adminToken).Count());
            reminder.UserId = 1;
            handler.EditReminder(1, reminder, adminToken);
            Assert.AreEqual(1, handler.Get(1, adminToken).Count());
            Assert.AreEqual(0, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void CannotEditNonExistantReminder()
        {
            var adminToken = GetAdminToken();
            SeedMedicine(adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => 
                handler.EditReminder(2, SeedReminder(1, adminToken), adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        [TestMethod]
        public void UserCannotEditOthersReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            var reminder = SeedReminder(1, adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.EditReminder(1, reminder, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void UserCanEditOwnReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            handler.EditReminder(1, reminder, userToken);
            Assert.AreEqual(500, handler.Get(2, userToken).First().DoseMg);
        }
        [TestMethod]
        public void UserCannotMoveReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            reminder.UserId = 1;
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.EditReminder(1, reminder, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void AdminCanDeleteAnyReminder()
        {
            var adminToken = GetAdminToken();
            GetUserToken("brownie");
            SeedMedicine(adminToken);
            SeedReminder(2, adminToken);
            Assert.AreEqual(1, handler.Get(2, adminToken).Count());
            handler.DeleteReminder(1, adminToken);
            Assert.AreEqual(0, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void UserCanDeleteOwnReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            SeedReminder(2, userToken);
            Assert.AreEqual(1, handler.Get(2, userToken).Count());
            handler.DeleteReminder(1, userToken);
            Assert.AreEqual(0, handler.Get(2, userToken).Count());
        }
        [TestMethod]
        public void UserCannotDeleteOthersReminder()
        {
            var adminToken = GetAdminToken();
            var userToken = GetUserToken("brownie");
            SeedMedicine(adminToken);
            SeedReminder(1, adminToken);
            Assert.AreEqual(1, handler.Get(1, adminToken).Count());
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.DeleteReminder(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void CannotDeleteNonExistantReminder()
        {
            var adminToken = GetAdminToken();
            SeedMedicine(adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.DeleteReminder(1, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        private void CreatePackageUnit(string token)
        {
            var packageUnit = new CreatePackageUnitDto() { Name = "packageUnit" };
            packageUnitHandler.CreatePackageUnit(packageUnit, token);
        }
        private void SeedMedicine(string adminToken)
        {
            CreatePackageUnit(adminToken);
            var medicine = new CreateMedicineDto()
            {
                Description = "uehotuoe",
                Manufacturer = "euohtnuhoe",
                Name = "ueohtnuheont",
                PackageUnitId = 1,
            };
            medicineHandler.CreateMedicine(medicine, adminToken);
        }
        private CreateReminderDto SeedReminder(int userId, string token)
        {
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                UserId = userId,
                When = "14:00:00"
            };
            handler.CreateReminder(reminder, token);
            return reminder;
        }
        private string GetUserToken(string username)
        {
            var user = new CreateUserDto() { UserName = username, Password = "Delulu!0" };
            userHandler.CreateUser(user);
            return userHandler.Login(user).Token;
        }
        private string GetAdminToken()
        {
            var admin = new CreateUserDto() { Password = "Delulu!0", UserName = "administrator" };
            userHandler.CreateUser(admin);
            return userHandler.Login(admin).Token;
        }
    }
}
