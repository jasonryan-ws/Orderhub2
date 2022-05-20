using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class GetPOsRequest : Token
    {
        // https://app.skuvault.com/api/purchaseorders/getPOs
        public DateTime ModifiedAfterDateTimeUtc { get; set; }
        public DateTime ModifiedBeforeDateTimeUtc { get; set; }

        // We will return all POs that are not completed by Default.
        // To get completed PO's, you must intentionally specify in the call.
        // Possible values are: NoneReceived, PartiallyReceived, Completed, Cancelled
        public POStatus Status { get; set; }
        // 10,000 POs are returned per page. Defaults to 0.
        public int PageNumber { get; set; } = 0;

        // Returns incoming product details in separate array.
        public bool IncludeProducts { get; set; }

        // A list of PO Numbers you can request data for. If using this parameter, the Date parameters and Status parameter are ignored.
        public List<string>? PONumbers { get; set; }
    }
}
