using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class BinModel
    {
        private Guid Id { get; set; }
        private string Name { get; set; }

        // Reserved bin will not appear on picklist by default
        private bool IsReserved { get; set; }
        // Default receiving location. Only single bin can be set as default
        private bool IsDefault { get; set; }
        private DateTime DateCreated { get; set; }
        private Guid CreatedByNodeId { get; set; }
        private DateTime DateModified { get; set; }
        private Guid? ModifiedByNodeId { get; set; }
        private bool IsDeleted { get; set; }
        private DateTime DateDeleted { get; set; }
        private Guid? DeletedByNodeId { get; set; }
    }
}
