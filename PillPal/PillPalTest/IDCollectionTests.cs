using Microsoft.EntityFrameworkCore;
using Moq;
using PillPalAPI.Model;
using PillPalLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PillPalTest
{

    [TestClass]
    public class IDCollectionTests
    {
        private DatabaseContext _context;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new DatabaseContext(options.Options);
        }

        [TestMethod]
        public void ItemsCanBeAdded()
        {
            var collection = new IDCollection<Medicine>(_context.Medicines);
            Assert.AreEqual(0, collection.Size);
            collection.Add(new Medicine("delulu", "hululu", "uehtoaeoutn", "uehton"));
            Assert.AreEqual(1, collection.Size);
        }

        [TestMethod]
        public void IndexerFindsExistingItems()
        {
            var collection = new IDCollection<Medicine>(_context.Medicines);
            collection.Add(new Medicine("Algoflex", "this is a medicine", "random menufacturer", "mg"));
            Assert.IsNotNull(collection[1]);
            Assert.IsNull(collection[10]);
        }

        [TestMethod]
        public void ItemCannotBeAddedTwice()
        {
            var collection = new IDCollection<Medicine>(_context.Medicines);
            Assert.AreEqual(0, collection.Size);
            var medicine = new Medicine("delulu", "hululu", "uehtoaeoutn", "uehton");
            Assert.IsTrue(collection.Add(medicine));
            Assert.IsFalse(collection.Add(medicine));
            Assert.AreEqual(1, collection.Size);
        }

        [TestMethod]
        public void ItemsCanBeRemoved()
        {
            var medicine = new Medicine("delulu", "hululu", "uehtoaeoutn", "uehton");
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            var added = _context.Medicines.Add(medicine);
            _context.SaveChanges();
            added.State = EntityState.Detached; // need to detach so we dont get tracking issues

            var collection = new IDCollection<Medicine>(_context.Medicines);
            Assert.AreEqual(1, collection.Size);
            Assert.IsTrue(collection.Remove(1));
            Assert.IsFalse(collection.Remove(2));
            Assert.AreEqual(0, collection.Size);
        }

        [TestMethod]
        public void ExistingItemCanBeUpdated()
        {
            var collection = new IDCollection<Medicine>(_context.Medicines);
            Assert.AreEqual(0, collection.Size);
            var medicine = new Medicine("delulu", "hululu", "uehtoaeoutn", "uehton");
            Assert.IsTrue(collection.Add(medicine));
            medicine.Name = "Test";
            Assert.IsTrue(collection.Replace(medicine));
            Assert.AreEqual("Test", collection.Values.ElementAt(0).Name);
        }

        [TestMethod]
        public void UpdatingOrDeletingNotExistingItemReturnsFalse()
        {
            var collection = new IDCollection<Medicine>(_context.Medicines);
            Assert.IsFalse(collection.Remove(0));
            Assert.IsFalse(collection.Replace(new Medicine("delulu", "hululu", "uehtoaeoutn", "uehton")));
        }
    }
}
