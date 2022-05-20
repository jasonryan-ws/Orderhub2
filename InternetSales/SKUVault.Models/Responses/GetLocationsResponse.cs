using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class GetLocationsResponse
    {
        public List<Location> Items { get; set; }
        public string Status { get; set; }
        public List<object> Errors { get; set; }
    }
}
