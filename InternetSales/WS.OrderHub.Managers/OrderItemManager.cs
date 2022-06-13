using Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.Managers
{
    public static class OrderItemManager
    {
        /// <summary>
        /// Get order items by order Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task<List<ProductModel>> GetByOrderIdAsync(Guid orderId)
        {
            try
            {
                var models = new List<ProductModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"SELECT
	                        p.*,
	                        i.Quantity,
	                        i.UnitPrice
                        FROM OrderItem i
                        JOIN [Order] o ON o.Id = i.OrderId
                        JOIN Product p ON p.Id = i.ProductId
                        WHERE o.Id = @OrderId";
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        var table= App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new ProductModel();
                            Fill(model, row);
                            models.Add(model);
                        }
                    }
                });
                return models;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Create a new order item with orderId and product model
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="product"></param>
        /// <param name="forceUpdate"></param>
        /// <param name="rollback"></param>
        /// <returns>Id of the newly created row</returns>
        public static async Task<Guid?> CreateAsync(Guid orderId, ProductModel product, bool? forceUpdate = null, bool rollback = true)
        {
            try
            {
                Guid? newId = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spOrderItem_Create
                        @Id OUTPUT,
                        @OrderId,
                        @ProductId,
                        @Quantity,
                        @UnitPrice,
                        @ForceUpdate";

                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@ProductId", product.Id);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                        command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                        App.SqlClient.ExecuteNonQuery(command, rollback);
                        newId = (Guid)id.Value;

                    }
                });
                return newId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Guid?> CreateAsync(Guid orderId, Guid productId, int quantity, decimal unitPrice, bool? forceUpdate = null, bool rollback = true)
        {
            try
            {
                Guid? newId = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spOrderItem_Create
                        @Id OUTPUT,
                        @OrderId,
                        @ProductId,
                        @Quantity,
                        @UnitPrice,
                        @ForceUpdate";

                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                        App.SqlClient.ExecuteNonQuery(command, rollback);
                        newId = (Guid)id.Value;

                    }
                });
                return newId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<int> UpdateAsync(Guid id, int quantity, decimal unitPrice, bool rollback = true)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spOrderItem_Update
                        @Id,
                        @Quantity,
                        @UnitPrice";
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        result= App.SqlClient.ExecuteNonQuery(command, rollback);
                    }
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<int> UpdateAsync(Guid orderId, Guid productId, int quantity, decimal unitPrice, bool rollback = true)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spOrderItem_UpdateByIds
                        @OrderId,
                        @ProductId,
                        @Quantity,
                        @UnitPrice";
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        result= App.SqlClient.ExecuteNonQuery(command, rollback);
                    }
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private static void Fill(ProductModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.SKU = Convert.ToString(row["SKU"]);
            model.UPC = Convert.ToString(row["UPC"]);
            model.Name = Convert.ToString(row["Name"]);
            model.ImageURL = Convert.ToString(row["ImageURL"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["CreatedByNodeId"]));
            model.DateModified = row["DateModified"] != DBNull.Value ? Convert.ToDateTime(row["DateModified"]) : null;
            model.ModifiedByNodeId = row["ModifiedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["ModifiedByNodeId"])) : null;
            model.IsDeleted = Convert.ToBoolean(row["IsDeleted"]);
            model.DateDeleted = row["DateDeleted"] != DBNull.Value ? Convert.ToDateTime(row["DateDeleted"]) : null;
            model.DeletedByNodeId = row["DeletedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["DeletedByNodeId"])) : null;
            model.Quantity = Convert.ToInt16(row["Quantity"]);
            model.UnitPrice = Convert.ToDecimal(row["UnitPrice"]);
        }
    }
}
