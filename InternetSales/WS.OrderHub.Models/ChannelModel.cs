using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class ChannelModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public Guid CreatedByNodeId { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid? ModifiedByNodeId { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? DeletedByNodeId { get; set; }
    }
}
