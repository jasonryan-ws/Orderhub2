using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Tests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetBySKUTest()
        {
            API.GetTokens();
            var results = ProductManager.Get("4017912126601").Result;
            Assert.AreEqual("GORE Thermo Overshoes - Black, 12.0-13.5", results.Description);
        }
        [TestMethod]
        public void GetByDateRangeTest()
        {
            API.GetTokens();
            var results = ProductManager.Get(new DateTime(2021,11,01), new DateTime(2021,12,01)).Result;
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetByCodesTest()
        {
            API.GetTokens();
            var results = ProductManager.Get(new List<string> { "836572001646", "812938013065" }).Result;
            Assert.AreEqual(2, results.Count);
        }
    }
}
