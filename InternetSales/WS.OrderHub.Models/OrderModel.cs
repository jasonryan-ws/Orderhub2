using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public DateTime DateOrdered { get; set; }
        public string ChannelOrderNumber { get; set; }
        public bool IsLocked { get; set; }
        public Guid? LockedByNodeId { get; set; }
        public Guid BillAddressId { get; set; }
        public Guid ShipAddressId { get; set; }
        public string Status { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateVerified { get; set; }
        public Guid? VerifiedByNodeId { get; set; }
        public string ShipMethod { get; set; }
        public bool IsShipped { get; set; }
        public DateTime? DateShipped { get; set; }
        public decimal ShipCost { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? DateCancelled { get; set; }
        public Guid? CancelledByNodeId { get; set; }
        public bool IsSkipped { get; set; }
        public DateTime? DateSkipped { get; set; }
        public Guid? SkippedByNodeId { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByNodeId { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedByNodeId { get; set; }
        public byte[] ExternalRowVersion { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Guid? DeletedByNodeId { get; set; }


        // Extended Properties
        public AddressModel BillAddress { get; set; }
        public AddressModel ShipAddress { get; set; }
        public ChannelModel Channel { get; set; }

        // Many-to-Many
        public List<ProductModel> Items { get; set; }
        public List<ChargeModel> Charges { get; set; }
    }
}
