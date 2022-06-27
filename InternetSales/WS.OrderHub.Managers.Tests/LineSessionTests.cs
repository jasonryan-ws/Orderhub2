using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class LineSessionTests
    {
        [TestMethod]
        public void CheckInTest()
        {
            var order = OrderManager.Get("13423496");
            var product = ProductManager.Get("8030282366664");
            var receivingQty = 13;
            string feedback;
            var result = LineSessionManager.CheckIn(order.Id, product.Id, receivingQty, out feedback, true);
            Console.WriteLine(feedback);
            Assert.AreEqual(204, result);

        }
    }
}
