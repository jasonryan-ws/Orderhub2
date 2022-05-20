using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SKUVault.Models
{
    public enum Response
    {
        Success,

        [Description("Bad Request")]
        BadRequest,

        [Description("Reason Not Found")]
        ReasonNotFound,

        [Description("Invalid Quantity")]
        InvalidQuantity,

        [Description("Unknown Error")]
        UnknownError,

        [Description("Product was not found in location")]
        ProductNotFoundInLocation,

        [Description("Product doesn't exist")]
        SkuNotFound,

        [Description("Not enough quantity")]
        NotEnoughQuantity,

        [Description("Location not found")]
        LocationNotFound,

        OK
    }
    public enum POStatus
    {
        [Description("Not Completed")]
        NotCompleted,
        [Description("None Received")]
        NoneReceived,
        [Description("Partially Received")]
        PartiallyReceived,
        [Description("Completed")]
        Completed,
        [Description("Cancelled")]
        Cancelled
    }
    public enum TransactionType { All, Add, Remove, Set, Pick, Create }
}
