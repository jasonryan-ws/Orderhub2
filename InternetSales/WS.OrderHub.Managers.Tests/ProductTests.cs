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
    public class ProductTests
    {
        [TestMethod]
        public void GetByIdAndSKUTest()
        {
            var model = ProductManager.GetAsync("4710944224184").Result;
            var result = ProductManager.GetAsync(model.Id).Result;
            Assert.AreEqual(result.Id, model.Id);
        }

        [TestMethod]
        public void GetWithLimit()
        {
            var result = ProductManager.GetAsync(2).Result;
            Assert.AreEqual(2, result.Count);         
        }

        [TestMethod]
        public void CreateTest()
        {
            var model = new ProductModel();
            model.SKU = "817966011168";
            model.UPC = "817966011168";
            model.Name = "Feedback Sports RAKK XL Display Stand - 1-Bike, Wheel Mount, 2.3-5\" Tire, Black";
            model.ImageURL = "https://productimages.qbp.com/6SPsvm45/prodxl/DS1844.jpg";
            model.CreatedByNodeId = NodeManager.ActiveNode.Id;
            var result = ProductManager.Create(model, null, true);
            Assert.AreNotEqual(Guid.Empty, model.Id);
        }


        [TestMethod]
        public void UpdateTest()
        {
            var model = ProductManager.GetAsync("643187000557").Result;
            model.SKU = "643187000557";
            model.UPC = "643187000557";
            model.Name = "Feedback Sports RAKK XL Display Stand - 1-Bike, Wheel Mount, 2.3-5\" Tire, Black";
            model.ImageURL = "https://productimages.qbp.com/6SPsvm45/prodxl/DS1844.jpg";
            model.CreatedByNodeId = NodeManager.ActiveNode.Id;
            var result = ProductManager.Update(model, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void SearchTest()
        {
            var result = ProductManager.SearchAsync("shi").Result;
            Assert.AreEqual(2, result.Count);
        }
    }
}
