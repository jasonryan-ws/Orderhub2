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
    public static class ProductBinManager
    {
        /// <summary>
        /// Get product bin row by Product ID and Bin ID
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="binId"></param>
        /// <returns>ProductModel</returns>
        public static ProductModel Get(Guid productId, Guid binId)
        {
            try
            {
                ProductModel model = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"
                        SELECT
	                        p.Id,
	                        p.SKU,
	                        p.UPC,
	                        p.[Name],
	                        p.ImageURL,
	                        pb.Quantity,
	                        pb.DateCreated,
	                        pb.CreatedByNodeId,
	                        pb.DateModified,
	                        pb.ModifiedByNodeId
                        FROM ProductBin pb
                        JOIN Product p ON p.Id = pb.ProductId
                        WHERE
                            pb.ProductId = @ProductId AND
                            pb.BinId = @BinId";
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@BinId", binId);
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new ProductModel();
                        Fill(model, row);
                    }
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get bin products by Bin ID
        /// </summary>
        /// <param name="binId"></param>
        /// <returns></returns>
        public static async Task<List<ProductModel>> GetProductsAsync(Guid binId)
        {
            try
            {
                var models = new List<ProductModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"
                        SELECT
	                        p.Id,
	                        p.SKU,
	                        p.UPC,
	                        p.[Name],
	                        p.ImageURL,
	                        pb.Quantity,
	                        pb.DateCreated,
	                        pb.CreatedByNodeId,
	                        pb.DateModified,
	                        pb.ModifiedByNodeId
                        FROM ProductBin pb
                        JOIN Product p ON p.Id = pb.ProductId
                        WHERE BinId = @BinId";
                        command.Parameters.AddWithValue("@BinId", binId);
                        var table = App.SqlClient.ExecuteQuery(command);
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
        /// Get product bins by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task<List<BinModel>> GetBinsAsync(Guid productId)
        {
            try
            {
                var models = new List<BinModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"
                        SELECT
	                        b.Id,
	                        b.[Name],
	                        b.IsReserved,
	                        b.IsDefault,
	                        pb.Quantity,
	                        pb.DateCreated,
	                        pb.CreatedByNodeId,
	                        pb.DateModified,
	                        pb.ModifiedByNodeId
                        FROM ProductBin pb
                        JOIN Bin b ON b.Id = pb.BinId
                        WHERE pb.ProductId = @ProductId";
                        command.Parameters.AddWithValue("@ProductId", productId);
                        var table = App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new BinModel();
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

        public static Guid? Create(Guid productId, Guid binId, int quantity, Guid createdByNodeId, bool? forceUpdate = null, bool rollback = false)
        {
            try
            {
                Guid? newId = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spProductBin_Create
                        @Id OUTPUT,
                        @ProductId,
                        @BinId,
                        @Quantity,
                        @CreatedByNodeId,
                        @ForceUpdate";
                    var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@BinId", binId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@CreatedByNodeId", createdByNodeId);
                    command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                    App.SqlClient.ExecuteNonQuery(command, rollback);
                    newId = (Guid)id.Value;
                }
                return newId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Guid productId, Guid binId, int quantity, Guid modifiedByNodeId, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"
                        DECLARE @Id UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM ProductBin WHERE ProductId = @ProductId AND BinId = @BinId);
                        EXEC spProductBin_Update
                        @Id,
                        @Quantity,
                        @ModifiedByNodeId";
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@BinId", binId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ModifiedByNodeId", modifiedByNodeId);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Guid id, int quantity, Guid modifiedByNodeId, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spProductBin_Create
                        @Id,
                        @Quantity,
                        @ModifiedByNodeId";
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ModifiedByNodeId", modifiedByNodeId);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid productId, Guid binId, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"
                        DECLARE @Id UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM ProductBin WHERE ProductId = @ProductId AND BinId = @BinId);
                        EXEC spProductBin_Delete @Id";
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@BinId", binId);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid id, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spProductBin_Delete @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);
                }
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
            model.Quantity = Convert.ToInt32(row["Quantity"]);
        }

        private static void Fill(BinModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.IsReserved = Convert.ToBoolean(row["IsReserved"]);
            model.IsDefault = Convert.ToBoolean(row["IsDefault"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["CreatedByNodeId"]));
            model.DateModified = row["DateModified"] != DBNull.Value ? Convert.ToDateTime(row["DateModified"]) : null;
            model.ModifiedByNodeId = row["ModifiedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["ModifiedByNodeId"])) : null;
            model.Quantity = Convert.ToInt32(row["Quantity"]);
        }
    }
}
