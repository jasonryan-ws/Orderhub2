using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void GetByIdAndNameTest()
        {
            var model = ConfigurationManager.GetAsync("SWServer").Result;
            var result = ConfigurationManager.GetAsync(model.Id).Result;
            Assert.AreEqual(model.Id, result.Id);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var result = ConfigurationManager.GetAsync().Result;
            Assert.AreEqual(result.Count, 11);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var model = ConfigurationManager.GetAsync("SWServer").Result;
            model.Value = "IS-SERVER";
            var result = ConfigurationManager.UpdateAsync(model, true).Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateByNameTest()
        {
            var result = ConfigurationManager.UpdateAsync("SWServer", "IS-SERVER", null, true).Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var value = ConfigurationManager.GetValue("SWIntegrated").Result;
            Assert.AreEqual("False", value);
        }
    }
}
