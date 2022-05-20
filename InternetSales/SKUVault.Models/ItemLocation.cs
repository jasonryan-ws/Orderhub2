using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models
{
    public class ItemLocation
    {
        public string SKU { get; set; }
        public string LocationCode { get; set; }
        public int Quantity { get; set; }
        public bool Reserve { get; set; }
        public string WarehouseCode { get; set; }
    }
}
