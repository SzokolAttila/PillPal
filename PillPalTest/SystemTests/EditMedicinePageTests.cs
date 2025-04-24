using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PillPalLib.StaticTools;
using PillPalLib.DTOs.UserDTOs;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.MedicineDTOs;
using OpenQA.Selenium.Support.UI;

namespace PillPalTest.SystemTests
{
    [TestClass]
    public class EditMedicinePageTests : SystemTestBase
    {
        MedicineAPIHandler medicineHandler = new MedicineAPIHandler();
        CreateMedicineDto?[] medicines = new CreateMedicineDto[2];

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            admin = handler.Login(adminLogin);
            driver.Navigate().GoToUrl(url);

            medicines = new CreateMedicineDto[]
            {
                new CreateMedicineDto()
                {
                    Name = "testmedicine",
                    Manufacturer = "somemanufacturer",
                    PackageUnitId = 1,
                    Description = "description"
                },
                new CreateMedicineDto()
                {
                    Name = "testmedicine2",
                    Manufacturer = "somemanufacturer2",
                    PackageUnitId = 1,
                    Description = "description2"
                }

            };
            AddMeds();
            
            driver.AwaitedFindElement(By.Name("username")).SendKeys(adminLogin.UserName);
            driver.FindElement(By.Name("password")).SendKeys(adminLogin.Password);
            driver.FindElement(By.Name("login")).Click();
            driver.AwaitedFindElement(By.Id("searchbar"));
            driver.Navigate().GoToUrl(url + "medicines/edit");
            driver.AwaitedFindElement(By.Name("name"));
            AllSectionsLoaded();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
            RemoveMeds();
        }

        [TestMethod]
        public void MedicineAlreadyLoadedIntoForm()
        {

            Assert.IsTrue(driver.FindElement(By.Name("name")).GetAttribute("value")!.Length > 0);
        }

        [TestMethod]
        public void ClickingOnAnotherMedicineLoadsThatMedicine()
        {
            var options = driver.FindElements(By.Name("medicineOption"));
            var name = options[1].Text;
            options[1].Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            Assert.AreEqual(name, driver.FindElement(By.Name("name")).GetAttribute("value"));
        }

        [TestMethod]
        public void MedicineDataNeedsToBeCorrectLength()
        {
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("aasdasdassdassdasdsadasdsadsaydasdyasd");
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("a");
            driver.FindElement(By.Id("updateMedBtn")).Click();
            var errors = driver.FindElements(By.ClassName("formkit-message"));
            Assert.AreEqual(3, errors.Count);

        }

        [TestMethod]
        public void MedicineChangesInDBIfUpdated()
        {
            driver.FindElements(By.Name("medicineOption")).First(x=>x.Text=="testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            driver.FindElement(By.Name("name")).Clear();
            driver.FindElement(By.Name("name")).SendKeys("changedmedicine");
            var modify = driver.FindElement(By.Id("updateMedBtn"));
            modify.Click(); // for the first, it only clicks out of the input field
            modify.Click();

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.SwitchTo().Alert());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            medicines[0]!.Name = "changedmedicine";

            var medicinesNow = medicineHandler.GetMedicines().FirstOrDefault(x=>x.Name=="testmedicine");
            Assert.IsNull(medicinesNow);
            medicinesNow = medicineHandler.GetMedicines().FirstOrDefault(x => x.Name == "changedmedicine");
            Assert.IsNotNull(medicinesNow);
        }

        [TestMethod]
        public void MedicineCanBeDeleted()
        {
            driver.FindElements(By.Name("medicineOption")).First(x=>x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();
            var delete = driver.FindElement(By.Id("removeMedBtn"));
            delete.Click();
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.SwitchTo().Alert());
            IAlert confirm = driver.SwitchTo().Alert();
            confirm.Accept();
            Thread.Sleep(500);
            wait.Until(d => d.SwitchTo().Alert());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            var medicinesNow = medicineHandler.GetMedicines().FirstOrDefault(x => x.Name == "testmedicine");
            Assert.IsNull(medicinesNow);
            medicines[0] = null;
        }

        [TestMethod]
        public void SideEffectCanBeAdded()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int effectsBefore = driver.FindElements(By.Name("removeEffectBtn")).Count;
            driver.FindElement(By.Id("addEffectBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeEffectBtn"));
            Assert.AreEqual(effectsBefore + 1, deletes.Count);
            Assert.AreEqual(effectsBefore + 1, medicineHandler.GetMedicines()
                .First(x=>x.Name=="testmedicine").SideEffectObjects.Count);
        }

        [TestMethod]
        public void SideEffectCanBeDeleted()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int effectsBefore = driver.FindElements(By.Name("removeEffectBtn")).Count;
            driver.FindElement(By.Id("addEffectBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeEffectBtn"));
            Assert.AreEqual(effectsBefore + 1, deletes.Count);
            deletes.Last().Click();
            Thread.Sleep(3000);
            Assert.AreEqual(effectsBefore, driver.FindElements(By.Name("removeEffectBtn")).Count);
        }

        [TestMethod]
        public void ActiveIngredientCanBeAdded()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int ingredientsBefore = driver.FindElements(By.Name("removeIngredientBtn")).Count;
            driver.FindElement(By.Id("addIngredientBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeIngredientBtn"));
            Assert.AreEqual(ingredientsBefore + 1, deletes.Count);
            Assert.AreEqual(ingredientsBefore + 1, medicineHandler.GetMedicines()
                .First(x => x.Name == "testmedicine").ActiveIngredientObjects.Count);
        }

        [TestMethod]
        public void ActiveIngredientCanBeDeleted()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int ingredientsBefore = driver.FindElements(By.Name("removeIngredientBtn")).Count;
            driver.FindElement(By.Id("addIngredientBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeIngredientBtn"));
            Assert.AreEqual(ingredientsBefore + 1, deletes.Count);
            deletes.Last().Click();
            Thread.Sleep(3000);
            Assert.AreEqual(ingredientsBefore, driver.FindElements(By.Name("removeIngredientBtn")).Count);
        }

        [TestMethod]
        public void RemedyForCanBeAdded()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int ailmentsBefore = driver.FindElements(By.Name("removeAilmentBtn")).Count;
            driver.FindElement(By.Id("addAilmentBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeAilmentBtn"));
            Assert.AreEqual(ailmentsBefore + 1, deletes.Count);
            Assert.AreEqual(ailmentsBefore + 1, medicineHandler.GetMedicines()
                .First(x => x.Name == "testmedicine").RemedyForObjects.Count);
        }

        [TestMethod]
        public void RemedyForCanBeDeleted()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int ailmentsBefore = driver.FindElements(By.Name("removeAilmentBtn")).Count;
            driver.FindElement(By.Id("addAilmentBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeAilmentBtn"));
            Assert.AreEqual(ailmentsBefore + 1, deletes.Count);
            deletes.Last().Click();
            Thread.Sleep(3000);
            Assert.AreEqual(ailmentsBefore, driver.FindElements(By.Name("removeAilmentBtn")).Count);
        }

        [TestMethod]
        public void PackageSizeCanBeAddedAndModified()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int sizesBefore = driver.FindElements(By.Name("removeSizeBtn")).Count;
            driver.FindElement(By.Id("addSizeBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeSizeBtn"));
            Assert.AreEqual(sizesBefore + 1, deletes.Count);
            Assert.AreEqual(sizesBefore + 1, medicineHandler.GetMedicines()
                .First(x => x.Name == "testmedicine").PackageSizeObjects.Count);

            var size = driver.FindElements(By.Name("size")).Last();
            size.Clear();
            size.SendKeys("100");
            driver.FindElement(By.Id("addSizeBtn")).Click();
            Thread.Sleep(2000); // wait for request to run
            Assert.IsTrue(medicineHandler.GetMedicines().First(x=>x.Name=="testmedicine")
                .PackageSizeObjects.Select(x=>x.Size).Contains(100));
        }

        [TestMethod]
        public void PackageSizeCanBeDeleted()
        {
            driver.FindElements(By.Name("medicineOption")).First(x => x.Text == "testmedicine").Click();
            Thread.Sleep(1000); // give time to resolve clicking on scroll list
            AllSectionsLoaded();

            int sizesBefore = driver.FindElements(By.Name("removeSizeBtn")).Count;
            driver.FindElement(By.Id("addSizeBtn")).Click();
            var deletes = driver.AwaitedFindElements(By.Name("removeSizeBtn"));
            Assert.AreEqual(sizesBefore + 1, deletes.Count);
            deletes.Last().Click();
            Thread.Sleep(3000);
            Assert.AreEqual(sizesBefore, driver.FindElements(By.Name("removeSizeBtn")).Count);
        }

        private void AllSectionsLoaded()
        {
            driver.AwaitedFindElement(By.Id("addEffectBtn"));
            driver.AwaitedFindElement(By.Id("addIngredientBtn"));
            driver.AwaitedFindElement(By.Id("addAilmentBtn"));
            driver.AwaitedFindElement(By.Id("addSizeBtn"));
        }

        private void AddMeds()
        {
            foreach (var med in medicines)
            {
                if(med != null)
                    medicineHandler.CreateMedicine(med, admin.Token);
            }
        }

        private void RemoveMeds()
        {
            foreach (var med in medicines)
            {
                if(med != null)
                {
                    foreach (var medInDb in medicineHandler.GetMedicines().Where(x => x.Name == med.Name))
                    {
                        medicineHandler.DeleteMedicine(medInDb.Id, admin.Token);
                    }
                }
            }
        }
    }
}
