using SKUVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Responses
{
    public class GetInventoryByLocationResponse
    {

        public ItemWrapper Items { get; set; }
        public List<object> Errors { get; set; }
    }

    // { SKU-as-Key : Locations }
    public class ItemWrapper : Dictionary<string, List<Location>> { }

}
