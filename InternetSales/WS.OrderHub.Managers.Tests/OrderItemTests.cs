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
            var order = OrderManager.Get("1345756");
            var result = OrderItemManager.GetByOrderId(order.Id);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            var order = OrderManager.Get("1345756");
            var product = ProductManager.Get("689228389267");
            product.Quantity = 47;
            product.UnitPrice = 39.99m;
            var result = OrderItemManager.Create(order.Id, product, null, true);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void CreateWithIdsTest()
        {
            var order = OrderManager.Get("1345756");
            var product = ProductManager.Get("689228389267");
            var result = OrderItemManager.Create(order.Id, product.Id, 47, 34.99m, null, true);
            Assert.AreNotEqual(null, result);
        }


        [TestMethod]
        public void UpdateTest()
        {
            var order = OrderManager.Get("1345756");
            var product = ProductManager.Get("4710944224184");
            var result = OrderItemManager.Update(order.Id, product.Id, 58, 34.99m, false);
            Assert.IsTrue(result > 0);
        }
    }
}
