using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V133.Security;
using OpenQA.Selenium.Support.UI;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.StaticTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.SystemTests
{
    [TestClass]
    public class UsersPageTests
    {

        UserAPIHandler handler = new UserAPIHandler();
        LoginDto? admin;
        WebDriver driver;
        CreateUserDto[] users;
        CreateUserDto adminLogin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
        string url = "http://vm1.test:5173/";

        [TestInitialize]
        public void Initialize()
        {
            users = null; // Prevent cleanup from removing the users if this is not used
            driver = new ChromeDriver();
            admin = handler.Login(adminLogin);
            driver.Navigate().GoToUrl(url);
            driver.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver.FindElement(By.Name("password")).SendKeys(adminLogin.Password);
            driver.FindElement(By.Name("login")).Click();
            driver.AwaitedFindElement(By.Id("searchbar"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
            if (users != null)
            {
                RemoveUsers(users);
            }

            if (admin == null)
            {
                handler.CreateUser(adminLogin);
            }
        }

        [TestMethod]
        public void SearchBarFiltersUsers()
        {
            users = new CreateUserDto[]
            {
                new CreateUserDto { UserName = "userwitha", Password="aA1?aA1?" },
                new CreateUserDto { UserName = "userwithb", Password="aA1?aA1?" },
            };
            AddUsers(users);
            driver.Navigate().Refresh(); // refresh so it loads with new user
            var searchbar = driver.AwaitedFindElement(By.Id("searchbar"));

            var username = driver.FindElements(By.Name("username"));
            Assert.IsTrue(username.Select(x=>x.Text).FirstOrDefault(x => !x.Contains("a")) != null);
            
            searchbar.SendKeys("a");
            username = driver.FindElements(By.Name("username"));
            Assert.IsTrue(username.Select(x => x.Text).FirstOrDefault(x => !x.Contains("a")) == null);
        }

        [TestMethod]
        public void UserDeletionCanBeCancelled()
        {   
            var deleteButtons = driver.AwaitedFindElements(By.Name("removeBtn"));
            int rows = deleteButtons.Count;

            deleteButtons[0].Click();
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            wait.Until(d=>d.SwitchTo().Alert());
            IAlert confirm = driver.SwitchTo().Alert();
            confirm.Dismiss();
            
            deleteButtons = driver.AwaitedFindElements(By.Name("removeBtn"));
            Assert.AreEqual(rows, deleteButtons.Count);
        }

        [TestMethod]
        public void UserCanBeDeleted()
        {
            users = new CreateUserDto[]
            {
                new CreateUserDto { UserName = "userwitha", Password="aA1?aA1?" },
            };
            AddUsers(users);
            driver.Navigate().Refresh(); // refresh so it loads with new user
            driver.AwaitedFindElement(By.Id("searchbar")).SendKeys("userwitha");

            var deleteButtons = driver.FindElements(By.Name("removeBtn"));
            int rows = deleteButtons.Count;
            deleteButtons[0].Click();
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            // wait for confirmation dialog to appear
            wait.Until(d => d.SwitchTo().Alert());
            IAlert confirm = driver.SwitchTo().Alert();
            confirm.Accept();
            // wait for reassuring message to appear
            wait.Until(d => d.SwitchTo().Alert());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            deleteButtons = driver.FindElements(By.Name("removeBtn"));
            Assert.AreEqual(rows-1, deleteButtons.Count);
            users = null; // Prevent cleanup from removing the user which is already deleted
        }

        [TestMethod]
        public void AdminCanBeDeleted()
        {
            driver.AwaitedFindElement(By.Id("searchbar")).SendKeys("administrator");

            var deleteButtons = driver.FindElements(By.Name("removeBtn"));
            int rows = deleteButtons.Count;
            deleteButtons[0].Click();
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            // wait for confirmation dialog to appear
            wait.Until(d => d.SwitchTo().Alert());
            IAlert confirm = driver.SwitchTo().Alert();
            confirm.Accept();
            // wait for reassuring message to appear
            wait.Until(d => d.SwitchTo().Alert());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            deleteButtons = driver.FindElements(By.Name("removeBtn"));
            Assert.AreEqual(rows - 1, deleteButtons.Count);
            admin = null; // make cleanup add a new admin

            // wait for message alert
            wait.Until(d => d.SwitchTo().Alert());
            alert = driver.SwitchTo().Alert();
            alert.Accept();

            Assert.AreEqual(url, driver.Url); // check if redirected to login page
        }

        private void AddUsers(CreateUserDto[] users)
        {
            foreach (var user in users)
            {
                handler.CreateUser(user);
            }
        }

        private void RemoveUsers(CreateUserDto[] users)
        {
            foreach (var user in users)
            {
                var login = handler.Login(user);
                handler.DeleteUser(login.Id, admin.Token);
            }
        }
    }
}
