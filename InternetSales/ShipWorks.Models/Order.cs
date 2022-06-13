using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipWorks.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public string RequestedShipping { get; set; }
        public string LocalStatus { get; set; }
        public byte[] RowVersion { get; set; }

        public DateTime? DateShipped { get; set; }
        public decimal ShipCost { get; set; }
        
        public Address BillAddress { get; set; }
        public Address ShipAddress { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Item> Items { get; set; }
        public List<Charge> Charges { get; set; } 
        public List<Shipment> Shipments { get; set; } 

        public decimal SubTotalAmount { get { return Items.Sum(i => i.SubTotal); } }
        public decimal ChargesTotalAmount { get { return Charges.Sum(c => c.Amount); } }
        public decimal ShipmentTotalCost { get { return Shipments.Sum(s => s.Cost); } }
        public decimal TotalAmount { get { return SubTotalAmount + ChargesTotalAmount; } }
        
        public string Comments
        {
            get
            {
                var comments = string.Empty;
                foreach (var n in Notes)
                {
                    comments += n.Text + "\n\n";
                }

                return comments.Trim();
            }
        }

        public string ChannelName
        {
            get
            {
                if (BillAddress.Email.ToLower().Contains("amazon.com"))
                    return "Amazon";
                if (BillAddress.Email.ToLower().Contains("walmart.com"))
                    return "Walmart";
                else if (RequestedShipping.ToUpper().Contains("EBAY"))
                    return "eBay";
                else
                    return "Default";
            }
        }

        public bool IsCancelled
        {
            get
            {
                return
                        LocalStatus.ToUpper() == "CANCELLED" ||
                        LocalStatus.ToUpper() == "CANCELED" ||
                        LocalStatus.ToUpper() == "HOLD" ||
                        LocalStatus.ToUpper() == "VOIDED";
            }
        }

        public bool IsShipped
        {
            get
            {
                return
                        LocalStatus.ToUpper() == "SHIPPED" ||
                        LocalStatus.ToUpper().Contains("PICKED UP") ||
                        LocalStatus.ToUpper().Contains("TRANSFERRED");
            }
        }

    }
}
