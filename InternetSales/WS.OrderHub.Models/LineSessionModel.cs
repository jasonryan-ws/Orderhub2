using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class LineSessionModel
    {
        public Guid Id { get ; set; }
        public Guid ObjectId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByNodeId { get; set; }
    }
}
