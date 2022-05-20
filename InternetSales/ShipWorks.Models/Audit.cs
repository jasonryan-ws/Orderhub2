using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipWorks.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ComputerId { get; set; }
        public int ObjectId { get; set; }
        public string Reason { get; set; }
    }
}
