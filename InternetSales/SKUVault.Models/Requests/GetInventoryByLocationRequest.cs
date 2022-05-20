using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class GetInventoryByLocationRequest : GenericRequest
    {
        public bool IsReturnByCodes { get; set; }
        public List<string> ProductSKUs { get; set; }
        public List<string> ProductCodes { get; set; }
   
    }
}
