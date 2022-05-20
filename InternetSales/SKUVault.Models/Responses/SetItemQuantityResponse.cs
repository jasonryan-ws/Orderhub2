using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class SetItemQuantityResponse
    {
        public string SetItemQuantityStatusEnum { get; set; }
        public string Status { get; set; }
        public List<string> Errors { get; set; }

        public Response Result { get => (Response)Enum.Parse(typeof(Response), SetItemQuantityStatusEnum); }
    }
}
