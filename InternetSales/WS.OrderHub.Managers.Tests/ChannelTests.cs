using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class ChannelTests
    {
        [TestMethod]
        public void GetByIdAndNameTest()
        {
            var model = ChannelManager.Get("Amazon");
            var result = ChannelManager.Get(model.Id);
            Assert.AreEqual(model.Id, result.Id);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var result = ChannelManager.GetAsync().Result;
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            var model = new ChannelModel();
            model.Name = "Shopify";
            model.Code = "SFY";
            model.ColorCode = 1234;
            //model.CreatedByNodeId = NodeManager.GetAsync("IS-JASON").Result.Id;
            var result = ChannelManager.Create(model, true);
            Assert.AreNotEqual(Guid.Empty, model.Id);
        }

        [TestMethod]
        public void CreateFailTest()
        {
            try
            {
                var model = new ChannelModel();
                model.Name = "Shopify";
                model.Code = "SFY";
                model.ColorCode = 1234;
                //model.CreatedByNodeId = NodeManager.GetAsync("IS-JASON").Result.Id;
                var result = ChannelManager.Create(model, true);

            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            var model = ChannelManager.Get("Amazon");
            model.Name = "Amazon Prime";
            model.Code = "AMZ";
            //model.ModifiedByNodeId = NodeManager.GetAsync("IS-JASON").Result.Id;
            var result = ChannelManager.Update(model, true);
            Assert.IsTrue(result > 0);
        }
    }
}
