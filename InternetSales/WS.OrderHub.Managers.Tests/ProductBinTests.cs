﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers.Tests
{
    [TestClass]
    public class ProductBinTests
    {
        [TestMethod]
        public void GetTest()
        {
            var product = ProductManager.GetAsync("643187000557").Result;
            var bin = BinManager.GetAsync("TestBin-M").Result;
            var result = ProductBinManager.GetAsync(product.Id, bin.Id).Result;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GetBinsTest()
        {
            var product = ProductManager.GetAsync("689228389267").Result;
            var result = ProductBinManager.GetBinsAsync(product.Id).Result;
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetProductTest()
        {
            var bin = BinManager.GetAsync("TestBin-M").Result;
            var result = ProductBinManager.GetProductsAsync(bin.Id).Result;
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            var product = ProductManager.GetAsync("643187000557").Result;
            var bin = BinManager.GetAsync("TestBin-B").Result;
            var result = ProductBinManager.Create(product.Id, bin.Id, 32, NodeManager.ActiveNode.Id, null, false);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateByIdsTest()
        {
            var product = ProductManager.GetAsync("643187000557").Result;
            var bin = BinManager.GetAsync("TestBin-M").Result;
            var result = ProductBinManager.Update(product.Id, bin.Id, 50, NodeManager.ActiveNode.Id, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var product = ProductManager.GetAsync("643187000557").Result;
            var bin = BinManager.GetAsync("TestBin-B").Result;
            var result = ProductBinManager.Delete(product.Id, bin.Id, false);
            Assert.IsTrue(result > 0);
        }

    }
}
