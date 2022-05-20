using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{

    // https://app.skuvault.com/api/products/getProducts
    public class GetProductsResponse
    {
        public List<Product> Products { get; set; }
        public List<object> Errors { get; set; }
    }
}
