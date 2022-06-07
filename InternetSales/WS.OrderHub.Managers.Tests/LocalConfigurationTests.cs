using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class LocalConfigurationTests
    {
        [TestMethod]
        public  void SQLClientTest()
        {
            Assert.IsNotNull(App.SQLClient);
        }
    }
}
