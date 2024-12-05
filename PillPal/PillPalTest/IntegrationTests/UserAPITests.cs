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
            api = new TestWebAppFactory<PillPalAPI.Program>();
            handler = new UserAPIHandler(client: api.CreateClient());
        }

        [TestMethod]
        public void CreatingUserWithUniqueUserAndProperPasswordReturnsTrue()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
        }

        [TestMethod]
        public void CreatingUserWithShortUsernameThrowsArgumentException()
        {
            CreateUserDto shortUserName = new CreateUserDto() { UserName = "user", Password = "aA1?aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(shortUserName));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithSpecialCharacterUsernameThrowsArgumentException()
        {
            CreateUserDto usernameWithSpecialCharacters = new() { UserName = "(){}+]a", Password = "Delulu!0" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(usernameWithSpecialCharacters));
            Assert.AreEqual("Username can only contain letters and digits.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithLongUsernameThrowsArgumentException()
        {
            CreateUserDto longUserName = new CreateUserDto() { UserName = "thisisareallylongusername", Password = "aA1?aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(longUserName));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);
        }

        [TestMethod]
        public void CreatingDuplicatedUserThrowsArgumentException()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(user));
            Assert.AreEqual("Username already in use.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithShortPasswordThrowsException()
        {
            CreateUserDto shortPassword = new CreateUserDto() { UserName = "username1", Password = "aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(shortPassword));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoLowercaseInPasswordThrowsArgumentException()
        {
            CreateUserDto password_lowercase = new CreateUserDto() { UserName = "username2", Password = "AA1?AA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_lowercase));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoUppercaseInPasswordThrowsArgumentException()
        {
            CreateUserDto password_uppercase = new CreateUserDto() { UserName = "username3", Password = "aa1?aa1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_uppercase));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoNumberInPasswordThrowsArgumentException()
        {
            CreateUserDto password_number = new CreateUserDto() { UserName = "username4", Password = "aAA?aAA?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_number));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoSpecialInPasswordThrowsArgumentException()
        {
            CreateUserDto password_special = new CreateUserDto() { UserName = "username5", Password = "aA1AaA1A" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_special));
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
        }

        [TestMethod]
        public void AdminCanGetAllUsers() { 
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

        [TestMethod]
        public void UserCannotDeleteOtherUserData()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var user2 = new CreateUserDto() { UserName = "username2", Password = "aA1?aA1?" };
            handler.CreateUser(user2);
            var usertoken = handler.Login(user);

            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteUser(2, usertoken));
            Assert.AreEqual("Forbidden", message.Message);
        }

        [TestMethod]
        public void UserCanDeleteOwnData()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var usertoken = handler.Login(user);

            Assert.AreEqual("username", handler.GetUser(1, usertoken).UserName);
            handler.DeleteUser(1, usertoken);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUser(1, usertoken));
            Assert.AreEqual("Not Found", message.Message);
        }

        [TestMethod]
        public void AdminCanDeleteAnyUser()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            handler.DeleteUser(1, adminToken);
            Assert.AreEqual(1, handler.GetUsers(adminToken).Count());
            handler.DeleteUser(2, adminToken);
            Assert.AreEqual(0, handler.GetUsers(adminToken).Count());
        }

        [TestMethod]
        public void DeletingNonExistingIdThrowsException()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteUser(2, adminToken));
            Assert.AreEqual("Not Found", message.Message);
        }

        [TestMethod]
        public void UserCannotUpdateOtherUserData()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var user2 = new CreateUserDto() { UserName = "username2", Password = "aA1?aA1?" };
            handler.CreateUser(user2);
            var usertoken = handler.Login(user);

            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(2, user, usertoken));
            Assert.AreEqual("Forbidden", message.Message);
        }

        [TestMethod]
        public void UserCanUpdateOwnData()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var usertoken = handler.Login(user);

            Assert.AreEqual("username", handler.GetUser(1, usertoken).UserName);
            user.UserName = "felhasznalo";
            handler.UpdateUser(1, user, usertoken);
            Assert.AreEqual("felhasznalo", handler.GetUser(1, usertoken).UserName);
        }

        [TestMethod]
        public void AdminCanUpdateAnyUser()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            Assert.AreEqual("username", handler.GetUser(1, adminToken).UserName);
            user.UserName = "felhasznalo";
            handler.UpdateUser(1, user, adminToken);
            Assert.AreEqual("felhasznalo", handler.GetUser(1, adminToken).UserName);
        }

        [TestMethod]
        public void UpdatingNonExistingIdThrowsException()
        {
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(2, admin, adminToken));
            Assert.AreEqual("Not Found", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedShortUsernameThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.UserName = "user";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedUsernameWithSpecialCharacterThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.UserName = "[username??!?![";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken)); 
            Assert.AreEqual("Username can only contain letters and digits.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedLongUsernameThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.UserName = "thisisawaaaaaylongnameforauser";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithDuplicatedUsernameThrowsAException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.UserName = "administrator";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Username already in use.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedShortPasswordThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aA1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutUppercaseThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aa1?aa1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutLowercaseThrowsArgumentException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "AA1?AA1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutNumbersThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aAa?aAa?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutSpecialThrowsArgumentException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aA11aA11";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithProperChangedPasswordAndImproperChangedUsernameThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aA1?aA1?";
            user.UserName = "user";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your username needs to be between 6 and 20 characters.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithImproperChangedPasswordAndProperChangedUsernameThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            handler.CreateUser(admin);
            var adminToken = handler.Login(admin);

            user.Password = "aA1?";
            user.UserName = "username4";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("Your password needs to include at least 8 characters, both upper and lowercase letters, a number, and a special character (@$!%*?&).", message.Message);
        }
    }
}
