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
    public class ChargeTests
    {
        [TestMethod]
        public void GetByIdAndNameTest()
        {
            var model = ChargeManager.GetAsync("Tax").Result;
            var result = ChargeManager.GetAsync(model.Id).Result;
            Assert.AreEqual(model.Id, result.Id);
        }

        [TestMethod]
        public void GetAll()
        {
            var result = ChargeManager.GetAsync().Result;
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void CreateTest()
        {
            var model = new ChargeModel();
            model.Name = "Reimbursement";
            model.Description = "Reimbursement charge";
            //model.CreatedByNodeId = NodeManager.GetAsync("IS-SERVER").Result.Id;
            var result = ChargeManager.CreateAsync(model, null, true).Result;
            Assert.AreNotEqual(Guid.Empty, model.Id);
        }
    }
}
