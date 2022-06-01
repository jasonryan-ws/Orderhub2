using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class ChannelTests
    {
        [TestMethod]
        public void GetByIdTest()
        {
            var model = ChannelManager.Get("Amazon");
            var result = ChannelManager.Get();
        }
    }
}
