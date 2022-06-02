using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class ConfigurationModel
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public object Value { get; set; }
		public string Description { get; set; } // Label
		public string FullDescription { get; set; } // Tooltip
		public DateTime? DateModified { get; set; }
		public Guid? ModifiedByNodeId { get; set; }
	}
}
