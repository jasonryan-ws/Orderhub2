using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKUVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void AddItemTest()
        {
            API.GetTokens();
            var results = InventoryManager.AddItemQuantity("devdummysku01", 1402, "00-DEFAULT", 5, TransactionType.Add, "API Test").Result;
            Assert.AreEqual(Response.Success, results);
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            API.GetTokens();
            var results = InventoryManager.RemoveItemQuantity("devdummysku01", 1402, "00-DEFAULT", 5, TransactionType.Remove).Result;
            Assert.AreEqual(Response.Success, results);
        }

        [TestMethod]
        public void SetItemQuantityTest()
        {
            API.GetTokens();
            var results = InventoryManager.SetItemQuantity("devdummysku01", 1402, "00-DEFAULT", 0).Result;
            Assert.AreEqual(Response.Success, results);
        }
    }
}
