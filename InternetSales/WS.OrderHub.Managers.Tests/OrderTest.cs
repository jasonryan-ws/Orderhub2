using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void DeleteTest()
        {
            var id = Guid.NewGuid();
            var results = OrderManager.DeleteAsync(id, true).Result;
            Assert.IsTrue(results > 0);
        }
        [TestMethod]
        public void GetTest()
        {
            var results = OrderManager.Get().Result;
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var id = Guid.NewGuid();
            var results = OrderManager.Get(id).Result;
            Assert.AreNotEqual(null, results);
        }

    }
}