using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PillPalLib.StaticTools;
using SeleniumExtras.WaitHelpers;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.SystemTests
{
    [TestClass]
    public class NavbarTests : SystemTestBase
    {
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
        public void NavbarAppearsWithAllButtons()
        {
            var hyperlinks = driver!.AwaitedFindElements(By.TagName("a"));
            Assert.AreEqual(4, hyperlinks.Count);
            Assert.AreEqual("Felhasználók", hyperlinks[0].Text);
            Assert.AreEqual("Új gyógyszer", hyperlinks[1].Text);
            Assert.AreEqual("Új gyógyszeradatok", hyperlinks[2].Text);
            Assert.AreEqual("Gyógyszer szerkesztése", hyperlinks[3].Text);
            var buttons = driver!.AwaitedFindElements(By.TagName("button"));
            Assert.IsTrue(buttons.Any(x => x.Text == "Kijelentkezés"));
            Assert.AreEqual(3, driver!.AwaitedFindElements(By.TagName("img")).Count);
            Assert.AreEqual("PillPal", driver!.AwaitedFindElement(By.TagName("h5")).Text);
        }
        [TestMethod]
        public void CannotRedirectIfLoggedOut()
        {
            driver!.AwaitedFindElement(By.TagName("a")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Ehhez a művelethez be kell jelentkeznie!"));
            alert.Accept();
            Assert.AreEqual(url, driver!.Url);
        }
        [TestMethod]
        public void CanRedirectToUsersPage()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Felhasználók").Click();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Id("searchbar")).Displayed);
            Assert.AreEqual(url + "users", driver!.Url);
        }
        [TestMethod]
        public void CanRedirectToNewMedicinePage()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszer").Click();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Name("name")).Displayed);
            Assert.AreEqual(url + "medicines/create", driver!.Url);
        }
        [TestMethod]
        public void CanRedirectToNewMedicineDataPage()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Id("searchSideEffect")).Displayed);
            Assert.AreEqual(url + "newData", driver!.Url);
        }
        [TestMethod]
        public void CanRedirectToEditMedicinePage()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Gyógyszer szerkesztése").Click();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Id("medicineName")).Displayed);
            Assert.AreEqual(url + "medicines/edit", driver!.Url);
        }
        [TestMethod]
        public void CanLogout()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.FindElement(By.Id("logout")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Biztosan ki akar jelentkezni?"));
            alert.Accept();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Name("username")).Displayed);
            Assert.AreEqual(url, driver.Url);
        }
        [TestCleanup]
        public void Cleanup()
        {
            driver!.Quit();
            if (deleteAdmin)
            {
                var login = handler.Login(adminLogin);
                handler.DeleteUser(login.Id, login.Token);
                deleteAdmin = false;
            }
        }
        private void Login()
        {
            driver!.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver!.FindElement(By.Name("password")).SendKeys(adminLogin.Password);
            driver.FindElement(By.Name("login")).Click();
        }
    }
}
