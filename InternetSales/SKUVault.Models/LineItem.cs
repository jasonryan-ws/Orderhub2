using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models
{
    public class LineItem
    {
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int QuantityTo3PL { get; set; }
        public int ReceivedQuantity { get; set; }
        public int ReceivedQuantityTo3PL { get; set; }
        public decimal Cost { get; set; }
        public int RetailCost { get; set; }
        public string PrivateNotes { get; set; }
        public string PublicNotes { get; set; }
        public string Variant { get; set; }
        public string Identifier { get; set; }
    }
}
