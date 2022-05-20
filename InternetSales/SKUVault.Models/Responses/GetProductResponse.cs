using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class GetProductResponse
    {
        public Product Product { get; set; }
        public List<object> Errors { get; set; }
    }
}
