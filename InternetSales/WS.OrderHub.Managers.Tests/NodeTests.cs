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
            var result = NodeManager.Create(model);
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
            var result = NodeManager.Get("IS-AUTH-01");
            Assert.IsTrue(result.Description.Contains("IS"));
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var id = NodeManager.Get("IS-AUTH-01").Id;
            var result = NodeManager.Get(id);
            Assert.IsTrue(result.Description.Contains("IS"));
        }

        [TestMethod]
        public void DeleteTest()
        { 
            var model = NodeManager.Get("IS-JASON");
            model.IsDeleted = true;
            model.DateDeleted = DateTime.Now;
            //model.DeletedByNodeId = NodeManager.GetAsync("DT-JASON").Result.Id;
            var result = NodeManager.Delete(model);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetCurrentNodeId()
        {
            Assert.AreNotEqual(Guid.Empty, NodeManager.ActiveNode);
        }
    }
}
