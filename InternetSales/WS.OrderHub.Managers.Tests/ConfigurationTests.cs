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
            var model = ConfigurationManager.Get("SWServer");
            var result = ConfigurationManager.Get(model.Id);
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
            var model = ConfigurationManager.Get("SWServer");
            model.Value = "IS-SERVER";
            var result = ConfigurationManager.Update(model, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateByNameTest()
        {
            var result = ConfigurationManager.Update("SWServer", "IS-SERVER", null, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetValueTest()
        {
            var value = ConfigurationManager.GetValue("SWIntegrated");
            Assert.AreEqual("False", value);
        }
    }
}
