using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class GetProductRequest : Token
    {
        // https://app.skuvault.com/api/products/getProduct
        public string ProductSKU { get; set; }
        public string ProductCode { get; set; }

    }
}
