using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class JobModel
    {
        // TaskModel
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        // JobModel
        public Guid Id { get; set; }
        public DateTime DateStarted { get; set; }
        public Guid StartedByNodeId { get; set; }
        public int? Progress { get; set; }
        public string Message { get; set; }
        public DateTime? DateProgressed { get; set; }
        public bool IsFinished { get; set; }
        public DateTime? DateEnded { get; set; }
        public Guid? EndedByNodeId { get; set; }
    }
}
