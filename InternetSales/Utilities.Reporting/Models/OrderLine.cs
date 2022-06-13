using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Reporting.Models
{
    public class OrderLine
    {
        public int Quantity { get; set; }
        public string ItemSKU { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExtendedPrice { get => UnitPrice * Quantity; }

        public string ItemSKUandName
        {
            get { return $"[{ItemSKU}]\n{ItemName}"; }
        }
    }
}
