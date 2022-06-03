using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class OrderItemTests
    {
        [TestMethod]
        public void GetByOrderIdTest()
        {
            var order = OrderManager.GetAsync("1345756").Result;
            var result = OrderItemManager.GetByOrderIdAsync(order.Id).Result;
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            var order = OrderManager.GetAsync("1345756").Result;
            var product = ProductManager.GetAsync("689228389267").Result;
            product.Quantity = 47;
            product.UnitPrice = 39.99m;
            var result = OrderItemManager.CreateAsync(order.Id, product, null, true).Result;
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void CreateWithIdsTest()
        {
            var order = OrderManager.GetAsync("1345756").Result;
            var product = ProductManager.GetAsync("689228389267").Result;
            var result = OrderItemManager.CreateAsync(order.Id, product.Id, 47, 34.99m, null, true).Result;
            Assert.AreNotEqual(null, result);
        }


        [TestMethod]
        public void UpdateTest()
        {
            var order = OrderManager.GetAsync("1345756").Result;
            var product = ProductManager.GetAsync("4710944224184").Result;
            var result = OrderItemManager.UpdateAsync(order.Id, product.Id, 58, 34.99m, false).Result;
            Assert.IsTrue(result > 0);
        }
    }
}
