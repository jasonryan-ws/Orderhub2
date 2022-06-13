using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Reporting.Models
{
    public class PickItem
    {
        public int Line { get; set; }
        public string Bins { get; set; }
        public string SKU { get; set; }
        public int PickQty { get; set; }
        public string ProductName { get; set; }
        public int OrderCount { get; set; }
    }
}
