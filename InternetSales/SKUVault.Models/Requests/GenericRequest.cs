using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.Models.Requests
{
    public class GenericRequest: Token
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
