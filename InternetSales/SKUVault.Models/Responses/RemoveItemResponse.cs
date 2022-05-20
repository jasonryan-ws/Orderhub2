using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class RemoveItemResponse
    {
        public string RemoveItemStatus { get; set; }
        public Response Status { get => (Response)Enum.Parse(typeof(Response), RemoveItemStatus); }
    }
}
