using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class OrderTest
    {
        //[TestMethod]
        //public void DeleteTest()
        //{
        //    var id = Guid.NewGuid();
        //    var results = OrderManager.DeleteAsync(id, true).Result;
        //    Assert.IsTrue(results > 0);
        //}


        //[TestMethod]
        //public void GetTest()
        //{
        //    var results = OrderManager.Get().Result;
        //    Assert.AreEqual(0, results.Count);
        //}

        [TestMethod]
        public void GetByIdAndNameTest()
        {
            var model = OrderManager.Get("1345756", true);
            var result = OrderManager.Get(model.Id);
            Assert.AreEqual(result.Id, model.Id);
        }


        [TestMethod]
        public void GetWithItems()
        {
            var model = OrderManager.Get("1345756", true, true);
            Assert.AreEqual(3, model.Items.Count);
        }


    }
}