using Microsoft.AspNetCore.Mvc.Testing;

using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.IntegrationTests
{
    [TestClass]
    public class UserAPITests
    {
        private WebApplicationFactory<PillPalAPI.Program> api;
        private UserAPIHandler handler;
        [TestInitialize]
        public void Init()
        {
            api = new WebApplicationFactory<PillPalAPI.Program>();
            handler = new UserAPIHandler(client: api.CreateClient());
        }

        [TestMethod]
        public void LoginGivesBackNothingIfUserDoesntExist()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?"};
            Assert.AreEqual(handler.Login(user), null);
        }

        [TestMethod]
        public void CreatingUserWithUniqueUserAndProperPasswordReturnsTrue()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            Assert.IsTrue(handler.CreateUser(user));
        }

        [TestMethod]
        public void CreatingUserNotMeetingTheRequirementsReturnsFalse()
        {
            CreateUserDto shortUserName = new CreateUserDto() { UserName = "user", Password = "aA1?aA1?" };
            Assert.IsFalse(handler.CreateUser(shortUserName));

            CreateUserDto longUserName = new CreateUserDto() { UserName = "thisisareallylongusername", Password = "aA1?aA1?" };
            Assert.IsFalse(handler.CreateUser(longUserName));

            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            CreateUserDto duplicateUser = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            Assert.IsFalse(handler.CreateUser(duplicateUser));

            CreateUserDto shortPassword = new CreateUserDto() { UserName = "username1", Password = "aA1?" };
            Assert.IsFalse(handler.CreateUser(shortPassword));

            CreateUserDto password_lowercase = new CreateUserDto() { UserName = "username2", Password = "AA1?AA1?" };
            Assert.IsFalse(handler.CreateUser(password_lowercase));

            CreateUserDto password_uppercase = new CreateUserDto() { UserName = "username3", Password = "aa1?aa1?" };
            Assert.IsFalse(handler.CreateUser(password_uppercase));

            CreateUserDto password_number = new CreateUserDto() { UserName = "username4", Password = "aAA?aAA?" };
            Assert.IsFalse(handler.CreateUser(password_number));

            CreateUserDto password_special = new CreateUserDto() { UserName = "username5", Password = "aA1AaA1A" };
            Assert.IsFalse(handler.CreateUser(password_special));
        }

        [TestMethod]
        public void LoginExistingUserGivesBackToken()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            Assert.IsTrue(handler.Login(user).Length>0);
        }
    }
}
