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
        public static async Task<ProductModel> GetAsync(Guid productId, Guid binId)
        {
            try
            {
                ProductModel model = null;
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
                        WHERE
                            pb.ProductId = @ProductId AND
                            pb.BinId = @BinId";
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@BinId", binId);
                        var table= App.SQLClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        { 
                            model = new ProductModel();
                            Fill(model, row);
                        }
                    }
                });

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
                        var table = App.SQLClient.ExecuteQuery(command);
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
                        var table = App.SQLClient.ExecuteQuery(command);
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
