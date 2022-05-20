using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class GetProductsRequest : GenericRequest
    {
        public DateTime ModifiedAfterDateTimeUtc { get; set; }
        public DateTime ModifiedBeforeDateTimeUtc { get; set; }
        public List<string>? ProductCodes { get; set; }
        public List<string>? ProductSKUs { get; set; }
    }
}
