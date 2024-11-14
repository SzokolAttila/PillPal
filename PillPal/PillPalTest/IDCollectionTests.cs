using PillPalLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest
{
    [TestClass]
    public class IDCollectionTests
    {
        [TestMethod]
        public void IndexerFindsExistingItems()
        {
            var collection = new IDCollection<Medicine>([new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton")]);
            Assert.IsNotNull(collection[0]);
            Assert.IsNull(collection[1]);
        }
        [TestMethod]
        public void ItemsCanBeAdded()
        {
            var collection = new IDCollection<Medicine>([]);
            Assert.AreEqual(0, collection.Size);
            collection.Add(new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton"));
            Assert.AreEqual(1, collection.Size);
        }
        [TestMethod]
        public void ItemCannotBeAddedTwice()
        {
            var collection = new IDCollection<Medicine>([]);
            Assert.AreEqual(0, collection.Size);
            var medicine = new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton");
            Assert.IsTrue(collection.Add(medicine));
            Assert.IsFalse(collection.Add(medicine));
            Assert.AreEqual(1, collection.Size);
        }
        [TestMethod]
        public void ItemsCanBeRemoved()
        {
            var collection = new IDCollection<Medicine>([]);
            Assert.AreEqual(0, collection.Size);
            var medicine = new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton");
            Assert.IsTrue(collection.Add(medicine));
            Assert.IsTrue(collection.Remove(0));
            Assert.IsFalse(collection.Remove(1));
            Assert.AreEqual(0, collection.Size);
        }
        [TestMethod]
        public void ExistingItemCanBeUpdated()
        {
            var collection = new IDCollection<Medicine>([]);
            Assert.AreEqual(0, collection.Size);
            var medicine = new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton");
            Assert.IsTrue(collection.Add(medicine));
            medicine.Name = "Test";
            Assert.IsTrue(collection.Replace(medicine));
            Assert.AreEqual("Test", collection.Values.ElementAt(0).Name);
        }
        [TestMethod]
        public void UpdatingOrDeletingNotExistingItemReturnsFalse()
        {
            var collection = new IDCollection<Medicine>([]);
            Assert.IsFalse(collection.Remove(0));
            Assert.IsFalse(collection.Replace(new Medicine("delulu", "hululu", [], "uehtoaeoutn", [], [], [], "uehton")));
        }
    }
}
