using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models
{
    public class Location
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string LocationCode { get; set; }
        public object ContainerCode { get; set; }
        public object ParentLocation { get; set; }
        public int TotalQuantity { get; set; }

        // For Inventory request
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public bool Reserve { get; set; }
    }

    public class Locations
    {
        public List<Location> Items { get; set; }
    }
}
