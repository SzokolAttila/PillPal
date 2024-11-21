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
    }
}
