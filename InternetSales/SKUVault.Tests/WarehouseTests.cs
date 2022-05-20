using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKUVault.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Tests
{
    [TestClass]
    public class WarehouseTests
    {
        [TestMethod]
        public void GetTest()
        {
            try
            {
                API.GetTokens();
                var results = WarehouseManager.Get().Result;
                Assert.AreEqual(1, results.Count);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
