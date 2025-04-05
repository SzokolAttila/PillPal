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
            Assert.AreEqual("A felhasználónévnek 6 és 20 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithSpecialCharacterUsernameThrowsArgumentException()
        {
            CreateUserDto usernameWithSpecialCharacters = new() { UserName = "(){}+]a", Password = "Delulu!0" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(usernameWithSpecialCharacters));
            Assert.AreEqual("A felhasználónév csak betűket és számokat tartalmazhat.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithLongUsernameThrowsArgumentException()
        {
            CreateUserDto longUserName = new CreateUserDto() { UserName = "thisisareallylongusername", Password = "aA1?aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(longUserName));
            Assert.AreEqual("A felhasználónévnek 6 és 20 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void CreatingDuplicatedUserThrowsArgumentException()
        {
            CreateUserDto user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(user));
            Assert.AreEqual("A felhasználónév már létezik.", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithShortPasswordThrowsException()
        {
            CreateUserDto shortPassword = new CreateUserDto() { UserName = "username1", Password = "aA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(shortPassword));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoLowercaseInPasswordThrowsArgumentException()
        {
            CreateUserDto password_lowercase = new CreateUserDto() { UserName = "username2", Password = "AA1?AA1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_lowercase));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoUppercaseInPasswordThrowsArgumentException()
        {
            CreateUserDto password_uppercase = new CreateUserDto() { UserName = "username3", Password = "aa1?aa1?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_uppercase));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoNumberInPasswordThrowsArgumentException()
        {
            CreateUserDto password_number = new CreateUserDto() { UserName = "username4", Password = "aAA?aAA?" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_number));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void CreatingUserWithNoSpecialInPasswordThrowsArgumentException()
        {
            CreateUserDto password_special = new CreateUserDto() { UserName = "username5", Password = "aA1AaA1A" };
            var message = Assert.ThrowsException<ArgumentException>(() => handler.CreateUser(password_special));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void LoginExistingUserGivesBackToken()
        {
            Assert.IsTrue(GetUserToken("brownie").Length > 0);
        }

        [TestMethod]
        public void InvalidUserLoginThrowsException()
        {
            var message = Assert.ThrowsException<ArgumentException>(() => handler.Login(new CreateUserDto()
            {
                UserName = "username",
                Password = "Hululu!0"
            }));
            Assert.AreEqual("Hibás felhasználónév vagy jelszó.", message.Message);
        }

        [TestMethod]
        public void GetAllUsersNeedsAuthorization()
        {
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUsers(""));
            Assert.AreEqual("Nincs bejelentkezve.", message.Message);
        }

        [TestMethod]
        public void AdminCanGetAllUsers() {
            var adminToken = GetAdminToken();
            var result = handler.GetUsers(adminToken);
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void AdminCanGetAnyUserData()
        {
            var adminToken = GetAdminToken();
            GetUserToken("brownie");
            Assert.AreEqual("brownie", handler.GetUser(2, adminToken).UserName);
            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
        }

        [TestMethod]
        public void UserCannotGetOtherUsersData()
        {
            var userToken = GetUserToken("brownie");
            GetUserToken("jackie");
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUser(2, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);

        }

        [TestMethod]
        public void UserCanGetOwnUserData()
        {
            var usertoken = GetUserToken("brownie");
            Assert.AreEqual("brownie", handler.GetUser(1, usertoken).UserName);
        }

        [TestMethod]
        public void UserCannotDeleteOtherUserData()
        {
            var userToken = GetUserToken("brownie");
            GetUserToken("jackie");
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteUser(2, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);
        }

        [TestMethod]
        public void UserCanDeleteOwnData()
        {
            var userToken = GetUserToken("username");

            Assert.AreEqual("username", handler.GetUser(1, userToken).UserName);
            handler.DeleteUser(1, userToken);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.GetUser(1, userToken));
            Assert.AreEqual("Nem található.", message.Message);
        }

        [TestMethod]
        public void AdminCanDeleteAnyUser()
        {
            GetUserToken("brownie");
            var adminToken = GetAdminToken();
            handler.DeleteUser(1, adminToken);
            Assert.AreEqual(1, handler.GetUsers(adminToken).Count());
            handler.DeleteUser(2, adminToken);
            Assert.AreEqual(0, handler.GetUsers(adminToken).Count());
        }

        [TestMethod]
        public void DeletingNonExistingIdThrowsException()
        {
            var adminToken = GetAdminToken();
            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.DeleteUser(2, adminToken));
            Assert.AreEqual("Nem található.", message.Message);
        }

        [TestMethod]
        public void UserCannotUpdateOtherUserData()
        {
            var userToken = GetUserToken("brownie");
            GetUserToken("jackie");
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(2, 
                new CreateUserDto() { UserName = "uetona", Password = "Delulu!0"}, userToken));
            Assert.AreEqual("Hozzáférés megtagadva.", message.Message);
        }

        [TestMethod]
        public void UserCanUpdateOwnData()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "Delulu!0" };
            var userToken = GetUserToken("username");
            Assert.AreEqual("username", handler.GetUser(1, userToken).UserName);
            user.UserName = "felhasznalo";
            handler.UpdateUser(1, user, userToken);
            Assert.AreEqual("felhasznalo", handler.GetUser(1, userToken).UserName);
        }

        [TestMethod]
        public void AdminCanUpdateAnyUser()
        {
            GetUserToken("username");
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "Delulu!0" };
            Assert.AreEqual("username", handler.GetUser(1, adminToken).UserName);
            user.UserName = "felhasznalo";
            handler.UpdateUser(1, user, adminToken);
            Assert.AreEqual("felhasznalo", handler.GetUser(1, adminToken).UserName);
        }

        [TestMethod]
        public void UpdatingNonExistingIdThrowsException()
        {
            var adminToken = GetAdminToken();
            var admin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
            Assert.AreEqual("administrator", handler.GetUser(1, adminToken).UserName);
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(2, admin, adminToken));
            Assert.AreEqual("Nem található.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedShortUsernameThrowsException()
        {
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);

            user.UserName = "user";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A felhasználónévnek 6 és 20 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedUsernameWithSpecialCharacterThrowsException()
        {
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);

            user.UserName = "[username??!?![";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken)); 
            Assert.AreEqual("A felhasználónév csak betűket és számokat tartalmazhat.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedLongUsernameThrowsException()
        {
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            user.UserName = "thisisawaaaaaylongnameforauser";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A felhasználónévnek 6 és 20 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithDuplicatedUsernameThrowsAException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.UserName = "administrator";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A felhasználónév már létezik.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedShortPasswordThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.Password = "aA1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutUppercaseThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.Password = "aa1?aa1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutLowercaseThrowsArgumentException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.Password = "AA1?AA1?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutNumbersThrowsException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.Password = "aAa?aAa?";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithChangedPasswordWithoutSpecialThrowsArgumentException()
        {
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            var adminToken = GetAdminToken();
            user.Password = "aA11aA11";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithProperChangedPasswordAndImproperChangedUsernameThrowsException()
        {
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            user.Password = "aA1?aA1?";
            user.UserName = "user";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A felhasználónévnek 6 és 20 karakter között kell lennie.", message.Message);
        }

        [TestMethod]
        public void PuttingUserWithImproperChangedPasswordAndProperChangedUsernameThrowsException()
        {
            var adminToken = GetAdminToken();
            var user = new CreateUserDto() { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            user.Password = "aA1?";
            user.UserName = "username4";
            var message = Assert.ThrowsException<ArgumentException>(() => handler.UpdateUser(1, user, adminToken));
            Assert.AreEqual("A jelszavadnak legalább 8 karakter hosszúnak kell lennie, tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).", message.Message);
        }
        private string GetAdminToken()
        {
            var admin = new CreateUserDto() { Password = "Delulu!0", UserName = "administrator" };
            handler.CreateUser(admin);
            return handler.Login(admin).Token;
        }
        private string GetUserToken(string username)
        {
            var user = new CreateUserDto() { UserName = username, Password = "Delulu!0" };
            handler.CreateUser(user);
            return handler.Login(user).Token;
        }
    }
}
