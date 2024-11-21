using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class ReminderAPITests
    {
        private WebApplicationFactory<PillPalAPI.Program> app;
        private ReminderAPIHandler handler;
        private UserAPIHandler userHandler;
        [TestInitialize]
        public void Init()
        {
            app = new WebApplicationFactory<PillPalAPI.Program>();
            handler = new ReminderAPIHandler(client: app.CreateClient());
            userHandler = new UserAPIHandler(client: app.CreateClient());
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
    }
}
