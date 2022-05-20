using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Tests
{
    [TestClass]
    public class SupplierTests
    {
        [TestMethod]
        public void GetTest()
        {
            API.GetTokens();
            var results = SupplierManager.Get().Result;
            Assert.AreEqual(461, results.Count);
        }
    }
}
