using PillPalLib;
using System.Diagnostics.CodeAnalysis;

namespace PillPalTest
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void IsHashUnique()
        {
            var user1 = new User("brownie", "Delulu!0");
            var user2 = new User("jackie", "Delulu!0");
            Assert.IsFalse(user1.Matches(user2.Password));
            user2 = new User("jackie", "Hululu!0");
            Assert.IsFalse(user1.Matches(user2.Password));
            user2 = new User("brownie", "Hululu!0");
            Assert.IsFalse(user1.Matches(user2.Password));
        }
        [TestMethod]
        public void DoHashesMatch()
        {
            var user1 = new User("brownie", "Delulu!0");
            var user2 = new User("brownie", "Delulu!0");
            Assert.IsTrue(user1.Matches(user2.Password));
        }
    }
}