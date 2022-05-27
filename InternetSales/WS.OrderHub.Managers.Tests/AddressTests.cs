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
    public class AddressTests
    {
        [TestMethod]
        public void GetTest()
        {
            var result = AddressManager.GetAsync().Result;
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void CreateTest()
        {
            var model = new AddressModel();
            model.FirstName = "Aldrin";
            model.MiddleName = "C";
            model.LastName = "Ybas";
            model.Company = "";
            model.Street1 = "3010 W Spencer St";
            model.Street2 = "Apt A15";
            model.Street3 = "";
            model.City = "Appleton";
            model.State = "WI";
            model.PostalCode = "54914";
            model.CountryCode = "US";
            model.Phone = "920-710-2660";
            model.Fax = "";
            model.Email = "aldrin.ybas@gmail.com";
            model.CreatedByNodeId = NodeManager.GetAsync("IS-JASON").Result.Id;

            var result = AddressManager.CreateAsync(model, true).Result;
            Assert.AreNotEqual(model.Id, Guid.Empty);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var model = AddressManager.GetAsync().Result.FirstOrDefault(a => a.LastName == "Ryan");
            model.LastName = "Ybas";
            model.ModifiedByNodeId = NodeManager.GetAsync("IS-JASON").Result.Id;

            var result = AddressManager.UpdateAsync(model, true).Result;
            Assert.IsTrue(result > 0);
           // Assert.AreNotEqual(model.Id, Guid.Empty);
        }


        [TestMethod]
        public void DeleteTest()
        {
            var result = AddressManager.DeleteUnusedAsync(true).Result;
            Assert.IsTrue(result > 0);
        }
    }
}
