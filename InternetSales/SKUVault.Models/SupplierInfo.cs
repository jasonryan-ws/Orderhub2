using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models
{
    public class SupplierInfo
    {
        public string SupplierName { get; set; }
        public string SupplierPartNumber { get; set; }
        public int Cost { get; set; }
        public int LeadTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrimary { get; set; }
    }
}
