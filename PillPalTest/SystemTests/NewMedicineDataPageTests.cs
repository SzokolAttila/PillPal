using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.PackageUnitDTOs;
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
    public class NewMedicineDataPageTests : SystemTestBase
    {
        private string? packageUnit = null;
        private string? sideEffect = null;
        private string? activeIngredient = null;
        private string? remedyFor = null;
        private bool deleteAdmin = false;
        private RemedyForAPIHandler remedyForHandler = new();
        private ActiveIngredientAPIHandler activeIngredientHandler = new ();
        private SideEffectAPIHandler sideEffectHandler = new ();
        private PackageUnitAPIHandler packageUnitHandler = new ();
        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            admin = handler.Login(adminLogin);
            if (admin == null)
            {
                handler.CreateUser(adminLogin);
                admin = handler.Login(adminLogin);
                deleteAdmin = true;
            }
        }
        [TestMethod]
        public void NewMedicineDataPageAppears()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            Assert.IsTrue(driver!.AwaitedFindElement(By.Id("searchSideEffect")).Displayed);
            Assert.AreEqual("Új gyógyszeradatok felvétele", driver!.FindElement(By.TagName("h1")).Text);
            Assert.IsTrue(driver!.FindElement(By.Id("searchActiveIngredient")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("searchRemedyFor")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("searchPackageUnit")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("packageUnitForm")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("activeIngredientForm")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("sideEffectForm")).Displayed);
            Assert.IsTrue(driver!.FindElement(By.Id("remedyForForm")).Displayed);
        }
        [TestMethod]
        public void NewPackageUnitAppears()
        {
            Login();
            packageUnit = "newPackageUnit";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver!.FindElement(By.Id("newPackageUnit")).SendKeys(packageUnit);
            driver!.FindElement(By.Id("submitNewPackageUnit")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("packageUnit")).Any(x => x.Text == packageUnit));
        }
        [TestMethod]
        public void CannotAddDuplicatePackageUnit()
        {
            Login();
            packageUnit = "newPackageUnit";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver!.FindElement(By.Id("newPackageUnit")).SendKeys(packageUnit);
            driver!.FindElement(By.Id("submitNewPackageUnit")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver!.FindElement(By.Id("newPackageUnit")).SendKeys(packageUnit);
            driver!.FindElement(By.Id("submitNewPackageUnit")).Click();
            var errorAlert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(errorAlert.Text!.Contains("Már létezik ez a mértékegység."));
            errorAlert.Accept();
        }
        [TestMethod]
        public void CannotCreatePackageUnitWithIncorrectLength()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver!.FindElement(By.Id("newPackageUnit")).SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            driver!.FindElement(By.Id("submitNewPackageUnit")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A mértékegység hossza 1 és 20 karakter között kell legyen.")));
        }
        [TestMethod]
        public void CanEditPackageSize()
        {
            Login();
            packageUnit = "newPackageUnit";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver!.FindElement(By.Id("newPackageUnit")).SendKeys(packageUnit);
            driver!.FindElement(By.Id("submitNewPackageUnit")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            driver.FindElements(By.ClassName("packageUnit")).First(x => x.Text == packageUnit).Click();
            packageUnit = "editedPackageUnit";
            driver!.FindElement(By.Id("existingPackageUnit")).Clear();
            driver!.FindElement(By.Id("existingPackageUnit")).SendKeys(packageUnit);
            driver!.FindElement(By.Id("editPackageUnit")).Click();
            alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres módosítás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("packageUnitForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("packageUnit")).Any(x => x.Text == packageUnit));
        }
        [TestMethod]
        public void NewSideEffectAppears()
        {
            Login();
            sideEffect = "newSideEffect";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver!.FindElement(By.Id("newSideEffect")).SendKeys(sideEffect);
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("sideEffect")).Any(x => x.Text == sideEffect));
        }
        [TestMethod]
        public void CannotAddDuplicateSideEffect()
        {
            Login();
            sideEffect = "newSideEffect";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver!.FindElement(By.Id("newSideEffect")).SendKeys(sideEffect);
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver!.FindElement(By.Id("newSideEffect")).SendKeys(sideEffect);
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            var errorAlert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(errorAlert.Text!.Contains("Már létezik ez a mellékhatás."));
            errorAlert.Accept();
        }
        [TestMethod]
        public void CannotCreateSideEffectWithIncorrectLength()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver!.FindElement(By.Id("newSideEffect")).SendKeys("aa");
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A mellékhatás hossza 3 és 80 karakter között kell legyen.")));
            driver!.FindElement(By.Id("newSideEffect")).Clear();
            driver!.FindElement(By.Id("newSideEffect")).SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A mellékhatás hossza 3 és 80 karakter között kell legyen.")));
        }
        [TestMethod]
        public void CanEditSideEffect()
        {
            Login();
            sideEffect = "newSideEffect";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver!.FindElement(By.Id("newSideEffect")).SendKeys(sideEffect);
            driver!.FindElement(By.Id("submitNewSideEffect")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            driver.FindElements(By.ClassName("sideEffect")).First(x => x.Text == sideEffect).Click();
            sideEffect = "editedSideEffect";
            driver!.FindElement(By.Id("existingSideEffect")).Clear();
            driver!.FindElement(By.Id("existingSideEffect")).SendKeys(sideEffect);
            driver!.FindElement(By.Id("editSideEffect")).Click();
            alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres módosítás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("sideEffectForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("sideEffect")).Any(x => x.Text == sideEffect));
        }
        [TestMethod]
        public void NewActiveIngredientAppears()
        {
            Login();
            activeIngredient = "newActiveIngredient";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys(activeIngredient);
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("activeIngredient")).Any(x => x.Text == activeIngredient));
        }
        [TestMethod]
        public void CannotAddDuplicateActiveIngredient()
        {
            Login();
            activeIngredient = "newActiveIngredient";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys(activeIngredient);
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys(activeIngredient);
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            var errorAlert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(errorAlert.Text!.Contains("Már létezik ez a hatóanyag."));
            errorAlert.Accept();
        }
        [TestMethod]
        public void CannotCreateActiveIngredientWithIncorrectLength()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys("aa");
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A hatóanyag hossza 3 és 80 karakter között kell legyen.")));
            driver!.FindElement(By.Id("newActiveIngredient")).Clear();
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A hatóanyag hossza 3 és 80 karakter között kell legyen.")));
        }
        [TestMethod]
        public void CanEditActiveIngredient()
        {
            Login();
            activeIngredient = "newActiveIngredient";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver!.FindElement(By.Id("newActiveIngredient")).SendKeys(activeIngredient);
            driver!.FindElement(By.Id("submitNewActiveIngredient")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            driver.FindElements(By.ClassName("activeIngredient")).First(x => x.Text == activeIngredient).Click();
            activeIngredient = "editedActiveIngredient";
            driver!.FindElement(By.Id("existingActiveIngredient")).Clear();
            driver!.FindElement(By.Id("existingActiveIngredient")).SendKeys(activeIngredient);
            driver!.FindElement(By.Id("editActiveIngredient")).Click();
            alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres módosítás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("activeIngredientForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("activeIngredient")).Any(x => x.Text == activeIngredient));
        }
        [TestMethod]
        public void NewRemedyForAppears()
        {
            Login();
            remedyFor = "newRemedyFor";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys(remedyFor);
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("remedyFor")).Any(x => x.Text == remedyFor));
        }
        [TestMethod]
        public void CannotAddDuplicateRemedyFor()
        {
            Login();
            remedyFor = "newRemedyFor";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys(remedyFor);
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys(remedyFor);
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            var errorAlert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(errorAlert.Text!.Contains("Már létezik ez a betegség."));
            errorAlert.Accept();
        }
        [TestMethod]
        public void CannotCreateRemedyForWithIncorrectLength()
        {
            Login();
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys("aa");
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A betegség hossza 3 és 80 karakter között kell legyen.")));
            driver!.FindElement(By.Id("newRemedyFor")).Clear();
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            Assert.IsTrue(driver!.AwaitedFindElements(By.ClassName("formkit-message")).Any(x => x.Text!.Contains("A betegség hossza 3 és 80 karakter között kell legyen.")));
        }
        [TestMethod]
        public void CanEditRemedyFor()
        {
            Login();
            remedyFor = "newRemedyFor";
            driver!.AwaitedFindElement(By.Id("searchbar"));
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver!.FindElement(By.Id("newRemedyFor")).SendKeys(remedyFor);
            driver!.FindElement(By.Id("submitNewRemedyFor")).Click();
            var alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres létrehozás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            driver.FindElements(By.ClassName("remedyFor")).First(x => x.Text == remedyFor).Click();
            remedyFor = "editedRemedyFor";
            driver!.FindElement(By.Id("existingRemedyFor")).Clear();
            driver!.FindElement(By.Id("existingRemedyFor")).SendKeys(remedyFor);
            driver!.FindElement(By.Id("editRemedyFor")).Click();
            alert = wait!.Until(ExpectedConditions.AlertIsPresent());
            Assert.IsTrue(alert.Text!.Contains("Sikeres módosítás."));
            alert.Accept();
            driver!.AwaitedFindElements(By.TagName("a")).First(x => x.Text == "Új gyógyszeradatok").Click();
            driver!.AwaitedFindElement(By.Id("remedyForForm"));
            Assert.IsTrue(driver.AwaitedFindElements(By.ClassName("remedyFor")).Any(x => x.Text == remedyFor));
        }
        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
            if (packageUnit != null)
            {
                var packageUnitId = packageUnitHandler.GetAll().First(x => x.Name == packageUnit).Id;
                packageUnitHandler.DeletePackageUnit(packageUnitId, admin!.Token);
                packageUnit = null;
            }
            if (sideEffect != null)
            {
                var sideEffectId = sideEffectHandler.GetAll().First(x => x.Effect == sideEffect).Id;
                sideEffectHandler.DeleteSideEffect(sideEffectId, admin!.Token);
                sideEffect = null;
            }
            if (activeIngredient != null)
            {
                var activeIngredientId = activeIngredientHandler.GetAll().First(x => x.Ingredient == activeIngredient).Id;
                activeIngredientHandler.DeleteActiveIngredient(activeIngredientId, admin!.Token);
                activeIngredient = null;
            }
            if (remedyFor != null)
            {
                var remedyForId = remedyForHandler.GetAll().First(x => x.Ailment == remedyFor).Id;
                remedyForHandler.DeleteRemedyFor(remedyForId, admin!.Token);
                remedyFor = null;
            }
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
