using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class BinModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Reserved bins will not appear on your picklist by default
        public bool IsReserved { get; set; }
        // Default receiving location. Only single bin can be set as default
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByNodeId { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedByNodeId { get; set; }
        public bool IsDeleted { get; set; }


        // Many-to-Many
        public List<ProductModel> Products { get; set; }

        // Extended properties
        public int Quantity { get; set; }
    }
}
