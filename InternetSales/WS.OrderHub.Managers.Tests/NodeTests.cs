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
    public class NodeTests
    {

        [TestMethod]
        public void CreateTest()
        {
            var model = new NodeModel();
            model.Name = "DT-JASON";
            model.Description = "Jason's personal computer";
            var result = NodeManager.CreateAsync(model).Result;
            Assert.AreNotEqual(model.Id, Guid.Empty);
        }


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

        [TestMethod]
        public void DeleteTest()
        { 
            var model = NodeManager.GetAsync("IS-JASON").Result;
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            model.DeletedByNodeId = NodeManager.GetAsync("DT-JASON").Result.Id;
            var result = NodeManager.DeleteAsync(model).Result;
            Assert.IsTrue(result > 0);
        }
    }
}
