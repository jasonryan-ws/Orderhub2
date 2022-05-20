using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ShipWorks.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void LoadTest()
        {
            Configuration.TestDatabaseCredentials();
            var orders = OrderManager.LoadAsync().Result;
            Assert.IsTrue(orders.Count > 1000);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Configuration.TestDatabaseCredentials();
            var order = OrderManager.LoadAsync(760541006).Result;
            Assert.IsTrue(order != null);
        }

        [TestMethod]
        public void LoadByOrderNumberExtendedTest()
        {
            Configuration.TestDatabaseCredentials();
            var order = OrderManager.LoadAsync("113133", true).Result;
            Assert.IsTrue(order.Notes.Count > 0);
        }

        [TestMethod]
        public void LoadByOrderNumbertest()
        {
            Configuration.TestDatabaseCredentials();
            var order = OrderManager.LoadAsync("113133").Result;
            Assert.IsTrue(order != null);
        }

        [TestMethod]
        public void LoadByMonthsAgoTest()
        {
            Configuration.TestDatabaseCredentials();
            var orders = OrderManager.LoadByMonthsAgoAsync(3).Result;
            Assert.IsTrue(orders.Count > 0);
        }

        [TestMethod]
        public void LoadByMonthsAgoExtendedTest()
        {
            var limit = 5;
            Configuration.TestDatabaseCredentials();
            var orders = OrderManager.LoadByMonthsAgoAsync(3, true, true, limit).Result;
            // Checks the first order if it contains at least one order item
            Assert.IsTrue(orders.FirstOrDefault().Items.Count > 0);
            Assert.AreEqual(limit, orders.Count);
        }

        [TestMethod]
        public void LoadByOrderDateRangeTest()
        {
            Configuration.TestDatabaseCredentials();
            var startDate = new DateTime(2021, 5, 3);
            var endDate = DateTime.Today;
            var orders = OrderManager.LoadAsync(startDate, endDate).Result;
            Assert.IsTrue(orders.Count > 1000);
        }
    }
}