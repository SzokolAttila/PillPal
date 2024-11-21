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

        [TestMethod]
        public void AdminRoleNeededToGetAllReminders()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(user);
            userHandler.CreateUser(admin);
            var userToken = userHandler.Login(user);
            var adminToken = userHandler.Login(admin);
            Assert.ThrowsException<ArgumentException>(() => handler.GetAll(userToken));
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
            Assert.ThrowsException<ArgumentException>(() => handler.Get(2, userToken));
        }
        [TestMethod]
        public void CannotAddReminderToNonExistantUser()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                UserId = 2,
                When = "14:00:00"
            };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            Assert.ThrowsException<ArgumentException>(() => handler.CreateReminder(reminder, adminToken));
        }
        [TestMethod]
        public void CannotAddReminderWithNonExistantMedicine()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "uoe",
                UserId = 1,
                When = "14:00:00"
            };
            userHandler.CreateUser(admin);
            var adminToken = userHandler.Login(admin);
            Assert.ThrowsException<ArgumentException>(() => handler.CreateReminder(reminder, adminToken));
        }
        [TestMethod]
        public void AdminCanCreateReminderForAnyone()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            userHandler.CreateUser(admin);
            userHandler.CreateUser(user);
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
            var adminToken = userHandler.Login(admin);
            medicineHandler.CreateMedicine(medicine, adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                When = "14:00:00",
                UserId = 1
            };
            handler.CreateReminder(reminder, adminToken);
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
            var adminToken = userHandler.Login(admin);
            medicineHandler.CreateMedicine(medicine, adminToken);
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                When = "14:00:00",
                UserId = 2
            }; 
            var userToken = userHandler.Login(user);
            Assert.ThrowsException<ArgumentException>(() => handler.CreateReminder(reminder, userToken));
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
            var reminder = new CreateReminderDto()
            {
                DoseCount = 1,
                DoseMg = 1,
                MedicineId = 1,
                TakingMethod = "ueo",
                When = "14:00:00",
                UserId = 2
            };
            medicineHandler.CreateMedicine(medicine, adminToken);
            handler.CreateReminder(reminder, userToken);
            Assert.AreEqual(1, handler.Get(2, userToken).Count());
        }
    }
}
