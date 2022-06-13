using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data;
using Utilities;
using ShipWorks.Models;
using Microsoft.Data.SqlClient;

namespace ShipWorks
{
    public static class OrderManager
    {
        private static string rawSqlStr = @"
                 SELECT
	                o.OrderID as 'Id',
	                o.OrderNumberComplete as 'OrderNumber',
	                o.OrderDate as 'DateOrdered',
	                o.RequestedShipping,
	                o.OnlineLastModified,
                    o.LocalStatus,
					o.RowVersion,

	                -- Bill Address
	                o.BillFirstName,
	                o.BillMiddleName,
	                o.BillLastName,
	                o.BillCompany,
	                o.BillStreet1,
	                o.BillStreet2,
	                o.BillStreet3,
	                o.BillCity,
					o.BillPostalCode,
	                o.BillStateProvCode,
	                o.BillCountryCode,
	                o.BillPhone,
	                o.BillFax,
	                o.BillEmail,
	                o.BillWebsite,

	                -- Ship Address
	                o.ShipFirstName,
	                o.ShipMiddleName,
	                o.ShipLastName,
	                o.ShipCompany,
	                o.ShipStreet1,
	                o.ShipStreet2,
	                o.ShipStreet3,
	                o.ShipCity,
					o.ShipPostalCode,
	                o.ShipStateProvCode,
	                o.ShipCountryCode,
	                o.ShipPhone,
	                o.ShipFax,
	                o.ShipEmail,
	                o.ShipWebsite,
					s.DateShipped,
					ISNULL(s.ShipCost, 0) as 'ShipCost'
                FROM [Order] o
				LEFT JOIN
				(
					SELECT
						OrderId,
						MAX(ProcessedDate) as 'DateShipped',
						SUM(ShipmentCost) as 'ShipCost'
						FROM Shipment
					WHERE
						Voided = 0 AND
						Processed = 1 AND
						TrackingNumber <> ''
					GROUP BY
						OrderId
				) s on s.OrderID = o.OrderID";


        /// <summary>
        /// Load by ShipWorks order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Order> GetAsync(int id, bool extended = false)
        {
            try
            {
                Order order = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = $@"{rawSqlStr} WHERE o.OrderId = @Id";
                        command.Parameters.AddWithValue("@Id", id);
                        var table = Configuration.Client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            order = new Order();
                            Fill(order, row);
                        }
                    }
                });
                // Load the extended info
                if (order != null && extended)
                {
                    await LoadExtendedAsync(order);
                }
                return order;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Load order by Order Number or sales channel order number (Amazon, eBay, Walmart, and etc)
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="extended">Set this to false if you only need basic order info to save resources.</param>
        /// <returns></returns>
        public static async Task<Order> GetAsync(string orderNumber, bool extended = false)
        {
            try
            {
                Order order = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = $@"{rawSqlStr} WHERE o.OrderNumberComplete = @OrderNumber";
                        command.Parameters.AddWithValue("@OrderNumber", orderNumber);
                        var table = Configuration.Client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            order = new Order();
                            Fill(order, row);

                        }
                    }
                });
                // Load the extended info
                if (order != null && extended)
                {
                    await LoadExtendedAsync(order);
                }
                return order;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Load orders asynchronously
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="maxRowVersion">Set to null to load all existing orders from ShipWorks server</param>
        /// <param name="months">Set to 0 to ignore and will return all the orders</param>
        /// <returns>List of orders</returns>
        public static async Task<List<Order>> GetAsync(bool extended = false, byte[]? maxRowVersion = null, int months = 0)
        {
            try
            {
                var orders = new List<Order>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        var condition = string.Empty;
                        if (maxRowVersion != null)
                        {
                            condition = "WHERE (o.RowVersion > @MaxRowVersion)";
                            command.Parameters.AddWithValue("@MaxRowVersion", maxRowVersion);
                        }

                        if (months > 0)
                        {
                            var date = DateTime.Today.AddMonths(months * -1);
                            if (string.IsNullOrWhiteSpace(condition))
                                condition = "WHERE o.OrderDate >= @Date";
                            else
                                condition += " AND o.OrderDate >= @Date";
                            command.Parameters.AddWithValue("@Date", date);
                        }
                        command.CommandText = $@"
                        {rawSqlStr}
                        {condition}
                        ORDER BY
	                        o.RowVersion";
                        var table = Configuration.Client.ExecuteQuery(command);

                        foreach (DataRow r in table.Rows)
                        {
                            var order = new Order();
                            Fill(order, r);
                            orders.Add(order);
                        }
                    }
                });

                // Load the extended info
                if (extended)
                {
                    await LoadExtendedAsync(orders);
                }

                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Load orders from months ago
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public static async Task<List<Order>> LoadByMonthsAgoAsync(int months, bool ascendingOrder = true, bool extended = false, int limit = 0)
        {
            try
            {
                var orders = new List<Order>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        var condition = string.Empty;
                        if (months > 0)
                        {
                            var date = DateTime.Today.AddMonths(months * -1);
                            condition = "WHERE o.OrderDate >= @Date";
                            command.Parameters.AddWithValue("@Date", date);
                        }

                        command.CommandText = $@"
                        {rawSqlStr}
                        {condition}
                        ORDER BY
	                        o.RowVersion {(ascendingOrder ? "ASC" : "DESC")}";
                        var table = Configuration.Client.ExecuteQuery(command);
                        var count = 0;
                        foreach (DataRow r in table.Rows)
                        {
                            var order = new Order();
                            Fill(order, r);
                            orders.Add(order);
                            if (limit > 0 && ++count >= limit)
                                break;
                        }
                    }
                });
                // Load the extended info
                if (extended)
                {
                    await LoadExtendedAsync(orders);
                }
                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Load order by order date range and sort by order date in ascending order by default
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="ascendingOrder">Determines the sorting order: true - ascdending, false: descending</param>
        /// <returns></returns>
        public static async Task<List<Order>> GetAsync(DateTime startDate, DateTime endDate, bool ascendingOrder = true, bool extended = false)
        {
            try
            {
                var orders = new List<Order>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = $@"
                        {rawSqlStr}
                        WHERE o.OrderDate BETWEEN @StartDate AND @EndDate
                        ORDER BY
	                        o.RowVersion {(ascendingOrder ? "ASC" : "DESC")}";
                        command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00.00"));
                        command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd 23:59:59.999"));
                        var table = Configuration.Client.ExecuteQuery(command);

                        foreach (DataRow r in table.Rows)
                        {
                            var order = new Order();
                            Fill(order, r);
                            orders.Add(order);
                        }
                    }
                });
                // Load the extended info
                if (extended)
                {
                    await LoadExtendedAsync(orders);
                }
                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Load the extended info
        public static async Task LoadExtendedAsync(Order order, bool loadItems = true, bool loadCharges = true, bool loadNotes = true)
        {
            try
            {
                if (loadItems)
                    order.Items = await ItemManager.LoadByOrderIdAsync(order.Id);
                if (loadNotes)
                    order.Notes = await NoteManager.LoadByObjectIdAsync(order.Id);
                if (loadCharges)
                    order.Charges = await ChargeManager.LoadByOrderIdAsync(order.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task LoadExtendedAsync(List<Order> orders, bool loadItems = true, bool loadCharges = true, bool loadNotes = true)
        {
            try
            {
                foreach (var order in orders)
                {
                    if (order != null)
                    {
                        await LoadExtendedAsync(order, loadItems, loadCharges, loadNotes);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static void Fill(Order order, DataRow row)
        {
            order.Id = Convert.ToInt32(row["Id"]);
            order.OrderNumber = Convert.ToString(row["OrderNumber"]);
            order.DateOrdered = Convert.ToDateTime(row["DateOrdered"]);
            order.RequestedShipping = Convert.ToString(row["RequestedShipping"]);
            order.LocalStatus = Convert.ToString(row["LocalStatus"]);
            if (row["RowVersion"] != DBNull.Value)
                order.RowVersion = (byte[])row["RowVersion"];
            if (row["DateShipped"] != DBNull.Value)
                order.DateShipped = Convert.ToDateTime(row["DateShipped"]);
            if (row["ShipCost"] != DBNull.Value)
                order.ShipCost = Convert.ToDecimal(row["ShipCost"]);
            order.BillAddress = new Address
            {
                FirstName = Convert.ToString(row["BillFirstName"]),
                MiddleName = Convert.ToString(row["BillMiddleName"]),
                LastName = Convert.ToString(row["BillLastName"]),
                Company = Convert.ToString(row["BillCompany"]),
                Street1 = Convert.ToString(row["BillStreet1"]),
                Street2 = Convert.ToString(row["BillStreet2"]),
                Street3 = Convert.ToString(row["BillStreet3"]),
                City = Convert.ToString(row["BillCity"]),
                PostalCode = Convert.ToString(row["BillPostalCode"]),
                StateCode = Convert.ToString(row["ShipStateProvCode"]),
                CountryCode = Convert.ToString(row["BillCountryCode"]),
                Phone = Convert.ToString(row["BillPhone"]),
                Fax = Convert.ToString(row["BillFax"]),
                Email = Convert.ToString(row["BillEmail"]),
                Website = Convert.ToString(row["BillWebsite"])
            };
            order.ShipAddress = new Address
            {
                FirstName = Convert.ToString(row["ShipFirstName"]),
                MiddleName = Convert.ToString(row["ShipMiddleName"]),
                LastName = Convert.ToString(row["ShipLastName"]),
                Company = Convert.ToString(row["ShipCompany"]),
                Street1 = Convert.ToString(row["ShipStreet1"]),
                Street2 = Convert.ToString(row["ShipStreet2"]),
                Street3 = Convert.ToString(row["ShipStreet3"]),
                City = Convert.ToString(row["ShipCity"]),
                PostalCode = Convert.ToString(row["ShipPostalCode"]),
                StateCode = Convert.ToString(row["ShipStateProvCode"]),
                CountryCode = Convert.ToString(row["ShipCountryCode"]),
                Phone = Convert.ToString(row["ShipPhone"]),
                Fax = Convert.ToString(row["ShipFax"]),
                Email = Convert.ToString(row["ShipEmail"]),
                Website = Convert.ToString(row["ShipWebsite"])
            };
        }
    }
}
