using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void GetAllTest()
        {
            var result = NodeManager.GetAsync().Result;
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetByNameTest()
        {
            var result = NodeManager.GetAsync("IS-JASON").Result;
            Assert.IsTrue(result.Description.Contains("Ryan"));
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var id = NodeManager.GetAsync("IS-JASON").Result.Id;
            var result = NodeManager.GetAsync(id).Result;
            Assert.IsTrue(result.Description.Contains("Ryan"));
        }
    }
}
