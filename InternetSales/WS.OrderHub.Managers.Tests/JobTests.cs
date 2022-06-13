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
    public class JobTests
    {
        [TestMethod]
        public void GetByIdTest()
        {
            var task = JobManager.Get().Result.FirstOrDefault();
            var result = JobManager.Get(task.Id).Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StartTest()
        {
            var model = new JobModel();
            model.TaskId = (Guid)TaskManager.Get("Update Orders").Result;
            model.StartedByNodeId = NodeManager.NodeId;
            var result = JobManager.StartAsync(model).Result;
            Assert.AreNotEqual(Guid.Empty, model.Id);
        }

    }
}
