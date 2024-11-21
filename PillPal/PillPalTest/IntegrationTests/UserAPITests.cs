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
        public void CreatingUserWithUniqueUserAndProperPasswordReturnsTrue()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
        }

        [TestMethod]
        public void CreatingUserNotMeetingTheRequirementsReturnsFalse()
        {
            CreateUserDto shortUserName = new CreateUserDto() { UserName = "user", Password = "aA1?aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(shortUserName));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);

            CreateUserDto usernameWithSpecialCharacters = new() { UserName = "(){}+]a", Password = "Delulu!0" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(usernameWithSpecialCharacters));
            Assert.AreEqual("Username can only contain letters and digits.", message.Message);

            CreateUserDto longUserName = new CreateUserDto() { UserName = "thisisareallylongusername", Password = "aA1?aA1?" };
            message = Assert.ThrowsException<ArgumentException>(()=>handler.CreateUser(longUserName));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);

            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            CreateUserDto duplicateUser = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(duplicateUser));
            Assert.AreEqual("Username already in use.", message.Message);

            CreateUserDto shortPassword = new CreateUserDto() { UserName = "username1", Password = "aA1?" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(shortPassword));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);

            CreateUserDto password_lowercase = new CreateUserDto() { UserName = "username2", Password = "AA1?AA1?" };
            message = Assert.ThrowsException<ArgumentException>(()=>handler.CreateUser(password_lowercase));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);

            CreateUserDto password_uppercase = new CreateUserDto() { UserName = "username3", Password = "aa1?aa1?" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_uppercase));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);

            CreateUserDto password_number = new CreateUserDto() { UserName = "username4", Password = "aAA?aAA?" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_number));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);

            CreateUserDto password_special = new CreateUserDto() { UserName = "username5", Password = "aA1AaA1A" };
            message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_special));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void LoginExistingUserGivesBackToken()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            Assert.IsTrue(handler.Login(user).Length > 0);
        }

        [TestMethod]
        public void InvalidUserLoginThrowsException()
        {
            var message = Assert.ThrowsException<ArgumentException>(() => handler.Login(new CreateUserDto()
            {
                UserName = "username",
                Password = "Hululu!0"
            }));
            Assert.AreEqual("Invalid username or password.", message.Message);
        }

        [TestMethod]
        public void GetAllUsersNeedsAuthorization()
        {
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUsers(""));
            Assert.AreEqual("Unauthorized", message.Message);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            handler.CreateUser(admin);
            var token = handler.Login(admin)!;
            var result = handler.GetUsers(token);
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void AdminCanGetAnyUserData()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "Delulu!0" };
            handler.CreateUser(admin);
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            handler.CreateUser(user);
            var adminToken = handler.Login(admin);
            Assert.AreEqual("brownie", handler.GetUser(2, adminToken).UserName);
            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
        }

        [TestMethod]
        public void UserCannotGetOtherUsersData()
        {
            var user1 = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            var user2 = new CreateUserDto() { UserName = "jackie", Password = "Delulu!0" };
            handler.CreateUser(user1);
            handler.CreateUser(user2);
            var user1token = handler.Login(user1)!;
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUser(2, user1token));
            Assert.AreEqual("Forbidden", message.Message);

        }

        [TestMethod]
        public void UserCanGetOwnUserData()
        {
            var user = new CreateUserDto() { UserName = "brownie", Password = "Delulu!0" };
            handler.CreateUser(user);
            var usertoken = handler.Login(user);
            Assert.AreEqual("brownie", handler.GetUser(1, usertoken).UserName);
        }
    }
}
