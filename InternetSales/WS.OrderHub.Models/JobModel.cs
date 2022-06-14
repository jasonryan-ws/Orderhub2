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

        public int Count { get; set; }
        public int MaxCount { get; set; }
        
        public string Message { get; set; }
        public DateTime? DateProgressionSet { get; set; }
        public bool? IsFinished { get; set; }
        public DateTime? DateEnded { get; set; }
        public Guid? EndedByNodeId { get; set; }


        // Get the progression in %
        public int Progress { get => MaxCount > 0 ? (Count * 100) / MaxCount: 0; }


        // Extended properties
        public NodeModel StartedbyNode { get; set; }
        public NodeModel EndedByNode { get; set; }
    }
}
