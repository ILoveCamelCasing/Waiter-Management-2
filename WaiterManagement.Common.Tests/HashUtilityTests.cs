using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaiterManagement.Common.Security;

namespace WaiterManagement.Common.Tests
{
    [TestClass]
    public class HashUtilityTests
    {
        [TestMethod]
        public void HashingTest()
        {
            var login = "login";
            var password = "password";

            var firstHash = HashUtility.CreateFirstHash(login, password);
            var secondHash = HashUtility.CreateSecondHash(firstHash);

            Assert.IsTrue(HashUtility.ValidatePassword(firstHash, secondHash));
        }
    }
}
