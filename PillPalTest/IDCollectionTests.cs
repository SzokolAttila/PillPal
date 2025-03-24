using Microsoft.EntityFrameworkCore;
using Moq;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.APIHandlers;
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
            var collection = new IDCollection<User>(_context.Users);
            Assert.AreEqual(0, collection.Size);
            collection.Add(new User("jackie", "Delulu!0"));
            Assert.AreEqual(1, collection.Size);
        }

        [TestMethod]
        public void IndexerFindsExistingItems()
        {
            var collection = new IDCollection<User>(_context.Users);
            collection.Add(new User("jackie", "Delulu!0"));
            Assert.IsNotNull(collection[1]);
            Assert.IsNull(collection[10]);
        }

        [TestMethod]
        public void ItemCannotBeAddedTwice()
        {
            var collection = new IDCollection<User>(_context.Users);
            Assert.AreEqual(0, collection.Size);
            var user = new User("jackie", "Delulu!0");
            Assert.IsTrue(collection.Add(user));
            Assert.IsFalse(collection.Add(user));
            Assert.AreEqual(1, collection.Size);
        }

        [TestMethod]
        public void ItemsCanBeRemoved()
        {
            var user = new User("jackie", "Delulu!0");
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
            var added = _context.Users.Add(user);
            _context.SaveChanges();
            added.State = EntityState.Detached; // need to detach so we dont get tracking issues

            var collection = new IDCollection<User>(_context.Users);
            Assert.AreEqual(1, collection.Size);
            Assert.IsTrue(collection.Remove(1));
            Assert.IsFalse(collection.Remove(2));
            Assert.AreEqual(0, collection.Size);
        }

        [TestMethod]
        public void ExistingItemCanBeUpdated()
        {
            var collection = new IDCollection<User>(_context.Users);
            Assert.AreEqual(0, collection.Size);
            var user = new User("jackie", "Delulu!0");
            Assert.IsTrue(collection.Add(user));
            user.UserName = "Test";
            Assert.IsTrue(collection.Replace(user));
            Assert.AreEqual("Test", collection.Values.ElementAt(0).UserName);
        }

        [TestMethod]
        public void UpdatingOrDeletingNotExistingItemReturnsFalse()
        {
            var collection = new IDCollection<User>(_context.Users);
            Assert.IsFalse(collection.Remove(0));
            Assert.IsFalse(collection.Replace(new User("jackie", "Delulu!0")));
        }
    }
}
