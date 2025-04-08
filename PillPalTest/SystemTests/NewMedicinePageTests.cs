using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.APIHandlers;
using PillPalLib.StaticTools;
using OpenQA.Selenium.Support.UI;

namespace PillPalTest.SystemTests
{
    [TestClass]
    public class NewMedicinePageTests
    {
        WebDriver driver;
        CreateUserDto adminLogin = new CreateUserDto() { UserName = "administrator", Password = "aA1?aA1?" };
        string url = "http://vm1.test:5173/";
        LoginDto admin;
        UserAPIHandler handler = new UserAPIHandler();
        MedicineAPIHandler medicineHandler = new MedicineAPIHandler();

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            admin = handler.Login(adminLogin);
            driver.Navigate().GoToUrl(url);
            driver.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver.FindElement(By.Name("password")).SendKeys(adminLogin.Password);
            driver.FindElement(By.Name("login")).Click();
            driver.AwaitedFindElement(By.Id("searchbar"));
            driver.Navigate().GoToUrl(url+"medicines/create");
            driver.AwaitedFindElement(By.Name("name"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void EmptyFormShowsFourErrors()
        {
            driver.FindElement(By.Name("createBtn")).Click();
            var errors = driver.FindElements(By.ClassName("formkit-message"));
            Assert.AreEqual(4, errors.Count);
        }

        [TestMethod]
        public void ErrorMessageShowsAtIncorrectLength()
        {
            driver.FindElement(By.Name("manufacturer")).SendKeys("a");
            driver.FindElement(By.Name("createBtn")).Click();
            var errors = driver.FindElements(By.ClassName("formkit-message"));
            Assert.IsTrue(errors.Select(x=>x.Text).Contains("A gyógyszer gyártójának hossza 5 és 30 karakter között kell legyen."));
        }

        [TestMethod]
        public void NewMedicineCanBeAdded()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            int medicinesBefore = medicineHandler.GetMedicines().Count();
            driver.FindElement(By.Name("name")).SendKeys("Mexalen");
            driver.FindElement(By.Name("manufacturer")).SendKeys("Teva");
            driver.FindElement(By.Name("description"))
                .SendKeys("Fájdalomcsillapító, lázcsökkentő hatású gyógyszer");
            var create = driver.FindElement(By.Name("createBtn"));
            create.Click(); // for the first, it only clicks out of the input field
            create.Click();

            wait.Until(d => d.SwitchTo().Alert());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            var medicinesNow = medicineHandler.GetMedicines();
            Assert.AreEqual(medicinesBefore+1, medicinesNow.Count());
            
            medicineHandler.DeleteMedicine(medicinesNow.First(x=>x.Name=="Mexalen").Id, admin.Token);
        }
    }
}
