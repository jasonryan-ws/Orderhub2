using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SKUVault.Models
{

    public class PurchaseOrder
    {
        public int PoId { get; set; }
        public string PoNumber { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string SentStatus { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderCancelDate { get; set; }
        public DateTime ArrivalDueDate { get; set; }
        public DateTime RequestedShipDate { get; set; }
        public DateTime ActualShippedDate { get; set; }
        public string TrackingInfo { get; set; }
        public string PublicNotes { get; set; }
        public string PrivateNotes { get; set; }
        public string TermsName { get; set; }
        public string ShipToWarehouse { get; set; }
        public string ShipToAddress { get; set; }
        public ShippingCarrierClass ShippingCarrierClass { get; set; }
        public List<decimal> Costs { get; set; }
        public List<LineItem> LineItems { get; set; }

        public POStatus POStatus { get => (POStatus)Enum.Parse(typeof(POStatus), Status); }
    }
}
