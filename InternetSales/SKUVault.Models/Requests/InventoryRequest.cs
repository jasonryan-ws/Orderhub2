using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class InventoryRequest : Token
    {
        public string Sku { get; set; }
        public string Code { get; set; }
        public int WarehouseId { get; set; }
        public string LocationCode { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public List<string> SerialNumbers { get; set; }
    }
}
