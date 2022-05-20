using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipWorks.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool IsVoided { get; set; }
        public DateTime DateVoided { get; set; }
        public string VoidedByUserName { get; set; }
        public string VoidedByComputerName { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime DateProcessed { get; set; }
        public string ProcessedByUserName { get; set; }
        public string ProcessedByComputerName { get; set; }
        public DateTime DateShipped { get; set; }
        public decimal Cost { get; set; }
        public string TrackingNumber { get; set; }
    }
}
