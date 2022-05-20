using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SKUVault.Models;
using SKUVault.Models.Requests;
using SKUVault.Models.Responses;
using System;
using System.Collections.Generic;

namespace SKUVault.Tests
{
    [TestClass]
    public class PurchaseOrderTests
    {
        [TestMethod]
        public void GetPOsByStatus()
        {
            API.GetTokens();
            var results = PurchaseOrderManager.Get(POStatus.NoneReceived).Result;
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPOsByDateRange()
        {
            API.GetTokens();
            var startDate = DateTime.Today.AddDays(-7);
            var endDate = DateTime.Today;
            var results = PurchaseOrderManager.Get(startDate, endDate).Result;
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPOsByPONumbers()
        {
            API.GetTokens();
            var results = PurchaseOrderManager.Get(new List<string> { "PO-712491-DS", "PO-712489" }).Result;
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void GetPOByPONumber()
        {
            API.GetTokens();
            var results = PurchaseOrderManager.Get("PO-712491-DS").Result;
            Assert.AreEqual("Quality Bicycle Products", results.SupplierName);
        }
    }
}