using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Tests
{
    [TestClass]
    public class LocationTests
    {

        [TestMethod]
        public void GetTest()
        {
            API.GetTokens();
            var results = LocationManager.Get().Result;
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetBySKUTest()
        {
            API.GetTokens();
            var results = LocationManager.GetBySKU("devdummysku01").Result;
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetBySKUsTest()
        {
            API.GetTokens();
            var results = LocationManager.GetBySKUs(new List<string> { "devdummysku01", "devdummysku02", "devdummysku03" }).Result;
            var locations = results.GetValueOrDefault("devdummysku01");
            Assert.AreEqual(locations.FirstOrDefault().LocationCode, "00-DEFAULT");
            //Assert.IsTrue(results.Count > 0);
        }
    }
}
