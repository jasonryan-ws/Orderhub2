using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedByNodeId { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? ModifiedByNodeId { get; set; }


        // Extended Properties

        public NodeModel CreatedByNode { get; set; }
        public NodeModel ModifiedByNode { get; set; }
    }
}
