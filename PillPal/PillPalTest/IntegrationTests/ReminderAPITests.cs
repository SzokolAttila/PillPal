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

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class ReminderAPITests
    {
        private WebApplicationFactory<PillPalAPI.Program> app;
        private ReminderAPIHandler handler;
        private UserAPIHandler userHandler;
        private MedicineAPIHandler medicineHandler;
        [TestInitialize]
        public void Init()
        {
            app = new WebApplicationFactory<PillPalAPI.Program>();
            handler = new ReminderAPIHandler(client: app.CreateClient());
            userHandler = new UserAPIHandler(client: app.CreateClient());
            medicineHandler = new MedicineAPIHandler(client: app.CreateClient());
        }
        private void SeedMedicine(string adminToken)
        {
            var medicine = new CreateMedicineDto()
            {
                ActiveIngredients = ["eota"],
                Description = "uehotuoe",
                Manufacturer = "euohtnuhoe",
                Name = "ueohtnuheont",
                PackageSizes = [10],
                PackageUnit = "mg",
                RemedyFor = ["ueohnteuot"],
                SideEffects = ["uehto"]
            };
            medicineHandler.CreateMedicine(medicine, adminToken);
        }
        private CreateReminderDto SeedReminder(int userId, string token)
        {
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                UserId = userId,
                When = "14:00:00"
            };
            handler.CreateReminder(reminder, token);
            return reminder;
        }
        [TestMethod]
        public void AdminRoleNeededToGetAllReminders()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            userHandler.CreateUser(admin);
            var userToken = userHandler.Login(user);
            var adminToken = userHandler.Login(admin);
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.GetAll(userToken));
            Assert.AreEqual("Forbidden", exception.Message);
            Assert.AreEqual(0, handler.GetAll(adminToken).Count());
        }
        [TestMethod]
        public void AdminCanGetUserReminders()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            Assert.AreEqual(0, handler.Get(1, adminToken).Count());
            Assert.AreEqual(0, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void UserCanGetOwnReminders()
        {
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            var userToken = userHandler.Login(user);
            Assert.AreEqual(0, handler.Get(1, userToken).Count());
        }
        [TestMethod]
        public void UserCannotGetOtherReminders()
        {
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            var user1 = new CreateUserDto() { UserName = "jackie", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            var userToken = userHandler.Login(user);
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.Get(2, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void DoseMgCannotBeNegativeOrZero()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseMg = 0,
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
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseMg = 1,
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
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(2, adminToken));
            Assert.AreEqual("User with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void CannotAddReminderWithNonExistantMedicine()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(1, adminToken));
            Assert.AreEqual("Medicine with the given ID doesn't exist.", exception.Message);
        }
        [TestMethod]
        public void AdminCanCreateReminderForAnyone()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
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
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            userHandler.CreateUser(admin); 
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var userToken = userHandler.Login(user);
            var exception = Assert.ThrowsException<ArgumentException>(() => SeedReminder(2, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void UserCanAddOwnReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            var userToken = userHandler.Login(user);
            SeedMedicine(adminToken);
            SeedReminder(2, userToken);
            Assert.AreEqual(1, handler.Get(2, userToken).Count());
        }
        [TestMethod]
        public void AdminCanEditAnyReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            reminder.TakingMethod = "take with water";
            reminder.DoseMg = 500;
            handler.EditReminder(1, reminder, adminToken);
            Assert.AreEqual(500, handler.Get(2, adminToken).First().DoseMg);
            Assert.AreEqual("take with water", handler.Get(2, adminToken).First().TakingMethod);
        }
        [TestMethod]
        public void AdminCanMoveReminders()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
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
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => 
                handler.EditReminder(2, SeedReminder(1, adminToken), adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
        [TestMethod]
        public void UserCannotEditOthersReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = SeedReminder(1, adminToken);
            var userToken = userHandler.Login(user);
            reminder.DoseMg = 500;
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.EditReminder(1, reminder, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void UserCanEditOwnReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            var userToken = userHandler.Login(user);
            reminder.DoseMg = 500;
            handler.EditReminder(1, reminder, userToken);
            Assert.AreEqual(500, handler.Get(2, userToken).First().DoseMg);
        }
        [TestMethod]
        public void UserCannotMoveReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var reminder = SeedReminder(2, adminToken);
            var userToken = userHandler.Login(user);
            reminder.UserId = 1;
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.EditReminder(1, reminder, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void AdminCanDeleteAnyReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            SeedReminder(2, adminToken);
            Assert.AreEqual(1, handler.Get(2, adminToken).Count());
            handler.DeleteReminder(1, adminToken);
            Assert.AreEqual(0, handler.Get(2, adminToken).Count());
        }
        [TestMethod]
        public void UserCanDeleteOwnReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            var userToken = userHandler.Login(user);
            SeedMedicine(adminToken);
            SeedReminder(2, userToken);
            Assert.AreEqual(1, handler.Get(2, userToken).Count());
            handler.DeleteReminder(1, userToken);
            Assert.AreEqual(0, handler.Get(2, userToken).Count());
        }
        [TestMethod]
        public void UserCannotDeleteOthersReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
            var adminToken = userHandler.Login(admin);
            var userToken = userHandler.Login(user);
            SeedMedicine(adminToken);
            SeedReminder(1, adminToken);
            Assert.AreEqual(1, handler.Get(1, adminToken).Count());
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.DeleteReminder(1, userToken));
            Assert.AreEqual("Forbidden", exception.Message);
        }
        [TestMethod]
        public void CannotDeleteNonExistantReminder()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            SeedMedicine(adminToken);
            var exception = Assert.ThrowsException<ArgumentException>(() => handler.DeleteReminder(1, adminToken));
            Assert.AreEqual("Not Found", exception.Message);
        }
    }
}
