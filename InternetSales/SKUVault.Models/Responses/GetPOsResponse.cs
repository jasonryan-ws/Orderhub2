using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class GetPOsResponse
    {
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<Product> Products { get; set; }
    }
}
