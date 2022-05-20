using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Sku { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Cost { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string WeightValue { get; set; }
        public string WeightUnit { get; set; }
        public int ReorderPoint { get; set; }
        public string Brand { get; set; }
        public string Supplier { get; set; }
        public List<SupplierInfo> SupplierInfo { get; set; }
        public string Classification { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityOnHold { get; set; }
        public int QuantityPicked { get; set; }
        public int QuantityPending { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityIncoming { get; set; }
        public int QuantityInbound { get; set; }
        public int QuantityTransfer { get; set; }
        public int QuantityInStock { get; set; }
        public int QuantityTotalFBA { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public List<object> Pictures { get; set; }
        public List<object> Attributes { get; set; }
        public string VariationParentSku { get; set; }
        public bool IsAlternateSKU { get; set; }
        public bool IsAlternateCode { get; set; }
        public int MOQ { get; set; }
        public string MOQInfo { get; set; }
        public int IncrementalQuantity { get; set; }
        public bool DisableQuantitySync { get; set; }
        public List<object> Statuses { get; set; }
        public bool IsSerialized { get; set; }
    }
}
