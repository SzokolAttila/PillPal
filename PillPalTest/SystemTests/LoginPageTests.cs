using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.StaticTools;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.SystemTests
{
    [TestClass]
    public class LoginPageTests : SystemTestBase
    {
        private CreateUserDto? user = null;
        private bool deleteAdmin = false;
        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Navigate().GoToUrl(url);
            admin = handler.Login(adminLogin);
            if (admin == null)
            {
                handler.CreateUser(adminLogin);
                admin = handler.Login(adminLogin);
                deleteAdmin = true;
            }  
        }
        [TestMethod]
        public void RootPageIsLoginPage()
        {
            Assert.AreEqual("Bejelentkezés", driver!.AwaitedFindElement(By.TagName("h1")).Text);
            Assert.IsTrue(driver!.FindElement(By.Name("username")).Displayed);
            Assert.IsTrue(driver.FindElement(By.Name("password")).Displayed);
            Assert.IsTrue(driver.FindElement(By.Name("login")).Displayed);
        }
        [TestMethod]
        public void IncorrectPasswordShowsError()
        {
            driver!.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver!.FindElement(By.Name("password")).SendKeys("wrongpassword");
            driver.FindElement(By.Name("login")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("400"));
            alert.Accept();
            Assert.AreEqual(url, driver.Url);
        }
        [TestMethod]
        public void OnlyAdminAllowed()
        {
            user = new CreateUserDto { UserName = "username", Password = "aA1?aA1?" };
            handler.CreateUser(user);
            driver!.AwaitedFindElement(By.Name("username")).SendKeys(user.UserName);
            driver!.FindElement(By.Name("password")).SendKeys(user.Password);
            driver.FindElement(By.Name("login")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Admin hozzáférésre van szüksége!"));
            alert.Accept();
            Assert.AreEqual(url, driver.Url);
        }
        [TestMethod]
        public void ValidLoginRedirectsToUsers()
        {
            driver!.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver!.FindElement(By.Name("password")).SendKeys(adminLogin.Password);
            driver.FindElement(By.Name("login")).Click();
            Assert.IsTrue(driver.AwaitedFindElement(By.Id("searchbar")).Displayed);
            Assert.AreEqual("Felhasználók", driver!.AwaitedFindElement(By.TagName("h1")).Text);
            Assert.AreEqual(url + "users", driver.Url);
        }
        [TestCleanup]
        public void Cleanup()
        {
            driver!.Quit();
            if (deleteAdmin)
            {
                RemoveUser(adminLogin);
                deleteAdmin = false; 
            }
            if (user != null)
            {
                RemoveUser(user);
                user = null;
            }
        }
        private void RemoveUser(CreateUserDto user)
        {
            var login = handler.Login(user);
            handler.DeleteUser(login.Id, login.Token);
        }
    }
}
