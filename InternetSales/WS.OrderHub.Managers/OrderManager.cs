using Data;
using Microsoft.Data.SqlClient;
using System.Data;
using WS.OrderHub.Models;

namespace WS.OrderHub.Managers
{
    public static class OrderManager
    {
        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="extended">Set to true to included address and channel info</param>
        /// <param name="includeItems">Set to true to include order items</param>
        /// <returns></returns>
        public static async Task<OrderModel> GetAsync(Guid id, bool extended = false, bool includeItems = false)
        {
            OrderModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"EXEC spOrder_GetById @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    var table= App.SQLClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new OrderModel();
                        Fill(model, row, extended, includeItems);
                    }
                }
            });
            return model;
        }
        /// <summary>
        /// Get order by channel order number
        /// </summary>
        /// <param name="channelOrderNumber"></param>
        /// <param name="extended">Set to true to included address and channel info</param>
        /// <param name="includeItems">Set to true to include order items</param>
        /// <returns></returns>
        public static async Task<OrderModel> GetAsync(string channelOrderNumber, bool extended = false, bool includeItems = false)
        {
            OrderModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"EXEC spOrder_GetByChannelOrderNumber @ChannelOrderNumber";
                    command.Parameters.AddWithValue("@ChannelOrderNumber", channelOrderNumber);
                    var table= App.SQLClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new OrderModel();
                        Fill(model, row, extended, includeItems);
                    }
                }
            });
            return model;
        }



        // Get Pending - Unverified, Unshipped, Not Cancelled
        // Get Unpicked - No pick history, unverified, unshipped, not cancelled
        // Get first unverified by Product SKU
        // Get verified but not shipped
        // Get shipped
        // Delete
        // Unlock
        // Unverify
        // Cancel
        // Verify


        /// <summary>
        /// Fill order model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="row"></param>
        /// <param name="row"></param>
        /// <param name="extended">Set to true if you want to fill billing address, shipping address and channel</param>
        /// /// <param name="includeItems">Set to true if you want include order items</param>
        private static void Fill(OrderModel model, DataRow row, bool extended = false, bool includeItems = false)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.ChannelId = Guid.Parse(Convert.ToString(row["ChannelId"]));
            model.DateOrdered = Convert.ToDateTime(row["DateOrdered"]);
            model.ChannelOrderNumber = Convert.ToString(row["ChannelOrderNumber"]);
            model.IsLocked = Convert.ToBoolean(row["IsLocked"]);
            if (row["LockedByNodeId"] != DBNull.Value)
                model.LockedByNodeId = Guid.Parse(Convert.ToString(row["LockedByNodeId"]));
            model.BillAddressId = Guid.Parse(Convert.ToString(row["BillAddressId"]));
            model.ShipAddressId = Guid.Parse(Convert.ToString(row["ShipAddressId"]));
            model.Status = Convert.ToString(row["Status"]);
            model.IsVerified = Convert.ToBoolean(row["IsVerified"]);
            if (row["DateVerified"] != DBNull.Value)
                model.DateVerified = Convert.ToDateTime(row["DateVerified"]);
            if (row["VerifiedByNodeId"] != DBNull.Value)
                model.VerifiedByNodeId = Guid.Parse(Convert.ToString(row["VerifiedByNodeId"]));
            model.ShipMethod = Convert.ToString(row["ShipMethod"]);
            model.IsShipped = Convert.ToBoolean(row["IsShipped"]);
            if (row["DateShipped"] != DBNull.Value)
                model.DateShipped = Convert.ToDateTime(row["DateShipped"]);
            if (row["ShipCost"] != DBNull.Value)
                model.ShipCost = Convert.ToDecimal(row["ShipCost"]);
            model.IsCancelled = Convert.ToBoolean(row["IsCancelled"]);
            if (row["DateCancelled"] != DBNull.Value)
                model.DateCancelled = Convert.ToDateTime(row["DateCancelled"]);
            if (row["CancelledByNodeId"] != DBNull.Value)
                model.CancelledByNodeId = Guid.Parse(Convert.ToString(row["CancelledByNodeId"]));
            model.IsSkipped = Convert.ToBoolean(row["IsSkipped"]);
            if (row["DateSkipped"] != DBNull.Value)
                model.DateSkipped = Convert.ToDateTime(row["DateSkipped"]);
            if (row["SkippedByNodeId"] != DBNull.Value)
                model.SkippedByNodeId = Guid.Parse(Convert.ToString(row["SkippedByNodeId"]));
            model.Comments = Convert.ToString(row["Comments"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["CreatedByNodeId"]));
            if (row["DateModified"] != DBNull.Value)
                model.DateModified = Convert.ToDateTime(row["DateModified"]);
            if (row["ModifiedByNodeId"] != DBNull.Value)
                model.ModifiedByNodeId = Guid.Parse(Convert.ToString(row["ModifiedByNodeId"]));
            if (row["ExternalRowVersion"] != DBNull.Value)
                model.ExternalRowVersion = (byte[])row["ExternalRowVersion"];
            model.IsDeleted = Convert.ToBoolean(row["IsDeleted"]);
            if (row["DateDeleted"] != DBNull.Value)
                model.DateDeleted = Convert.ToDateTime(row["DateDeleted"]);
            if (row["DeletedByNodeId"] != DBNull.Value)
                model.DeletedByNodeId = Guid.Parse(Convert.ToString(row["DeletedByNodeId"]));

            if (extended)
            {
                model.Channel = new ChannelModel();
                model.Channel.Id = model.ChannelId;
                model.Channel.Name = Convert.ToString(row["ChannelName"]);
                model.Channel.Code = Convert.ToString(row["ChannelCode"]);
                model.Channel.ColorCode = row["ChannelColorCode"] != DBNull.Value ? Convert.ToInt32(row["ChannelColorCode"]) : null;


                model.BillAddress = new AddressModel();
                model.BillAddress.Id = model.BillAddressId;
                model.BillAddress.FirstName = Convert.ToString(row["BillFirstName"]);
                model.BillAddress.MiddleName = Convert.ToString(row["BillMiddleName"]);
                model.BillAddress.LastName = Convert.ToString(row["BillLastName"]);
                model.BillAddress.Company = Convert.ToString(row["BillCompany"]);
                model.BillAddress.Street1 = Convert.ToString(row["BillStreet1"]);
                model.BillAddress.Street2 = Convert.ToString(row["BillStreet2"]);
                model.BillAddress.Street3 = Convert.ToString(row["BillStreet3"]);
                model.BillAddress.City = Convert.ToString(row["BillCity"]);
                model.BillAddress.State = Convert.ToString(row["BillStateCode"]);
                model.BillAddress.State = Convert.ToString(row["BillStateName"]);
                model.BillAddress.PostalCode = Convert.ToString(row["BillPostalCode"]);
                model.BillAddress.CountryCode = Convert.ToString(row["BillCountryCode"]);
                model.BillAddress.CountryCode = Convert.ToString(row["BillCountryName"]);
                model.BillAddress.Phone = Convert.ToString(row["BillPhone"]);
                model.BillAddress.Fax = Convert.ToString(row["BillFax"]);
                model.BillAddress.Email = Convert.ToString(row["BillEmail"]);

                model.ShipAddress = new AddressModel();
                model.ShipAddress.Id = model.ShipAddressId;
                model.ShipAddress.FirstName = Convert.ToString(row["ShipFirstName"]);
                model.ShipAddress.MiddleName = Convert.ToString(row["ShipMiddleName"]);
                model.ShipAddress.LastName = Convert.ToString(row["ShipLastName"]);
                model.ShipAddress.Company = Convert.ToString(row["ShipCompany"]);
                model.ShipAddress.Street1 = Convert.ToString(row["ShipStreet1"]);
                model.ShipAddress.Street2 = Convert.ToString(row["ShipStreet2"]);
                model.ShipAddress.Street3 = Convert.ToString(row["ShipStreet3"]);
                model.ShipAddress.City = Convert.ToString(row["ShipCity"]);
                model.ShipAddress.State = Convert.ToString(row["ShipStateCode"]);
                model.ShipAddress.State = Convert.ToString(row["ShipStateName"]);
                model.ShipAddress.PostalCode = Convert.ToString(row["ShipPostalCode"]);
                model.ShipAddress.CountryCode = Convert.ToString(row["ShipCountryCode"]);
                model.ShipAddress.CountryCode = Convert.ToString(row["ShipCountryName"]);
                model.ShipAddress.Phone = Convert.ToString(row["ShipPhone"]);
                model.ShipAddress.Fax = Convert.ToString(row["ShipFax"]);
                model.ShipAddress.Email = Convert.ToString(row["ShipEmail"]);
            }

            if (includeItems)
            {
                model.Items = OrderItemManager.GetByOrderIdAsync(model.Id).Result;
            }
        }
    }
}