using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByNodeId { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedByNodeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? DeletedByNodeId { get; set; }


        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } // Listing Price, Purchase Price
        public decimal Cost { get; set; }


        // Many-to-Many
        public List<OrderModel> Orders { get; set; }
        public List<BinModel> Bins { get; set; }
    }
}
