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
    public static class ProductManager
    {
        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ProductModel> GetAsync(Guid id)
        {
            ProductModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"SELECT * FROM Product WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
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

        /// <summary>
        /// Get product by SKU or UPC
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static async Task<ProductModel> GetAsync(string identifier)
        {
            ProductModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"SELECT * FROM Product WHERE SKU = @Identifier OR UPC = @Identifier";
                    command.Parameters.AddWithValue("@Identifier", identifier);
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

        /// <summary>
        /// Get the list of products
        /// </summary>
        /// <param name="limit">Limits the number of results. Set to 0 to ignore limitation.</param>
        /// <returns></returns>
        public static async Task<List<ProductModel>> GetAsync(int limit = 0)
        {
            var models = new List<ProductModel>();
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    var top = string.Empty;
                    if (limit > 0)
                    {
                        top = "TOP (@Limit)";
                        command.Parameters.AddWithValue("@Limit", limit);
                    }

                    command.CommandText = $@"SELECT {top} * FROM Product";
                    var table= App.SQLClient.ExecuteQuery(command);
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


        public static async Task<int> CreateAsync(ProductModel model, bool? forceUpdate = null, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spProduct_Create
                        @Id OUTPUT,
                        @SKU,
                        @UPC,
                        @Name,
                        @ImageURL,
                        @CreatedByNodeId,
                        @ForceUpdate";
                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@SKU", model.SKU);
                        command.Parameters.AddWithValue("@UPC", model.UPC);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@ImageURL", model.ImageURL);
                        if (model.CreatedByNodeId == Guid.Empty)
                            model.CreatedByNodeId = NodeManager.NodeId;
                        command.Parameters.AddWithValue("@CreatedByNodeId", model.CreatedByNodeId);
                        command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                        result= App.SQLClient.ExecuteNonQuery(command, rollback);
                        model.Id = (Guid)id.Value;
                    }
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<int> UpdateAsync(ProductModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spProduct_Update
                        @Id,
                        @SKU,
                        @UPC,
                        @Name,
                        @ImageURL,
                        @ModifiedByNodeId";
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@SKU", model.SKU);
                        command.Parameters.AddWithValue("@UPC", model.UPC);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@ImageURL", model.ImageURL);
                        if (model.ModifiedByNodeId == null)
                            model.ModifiedByNodeId = NodeManager.NodeId;
                        command.Parameters.AddWithValue("@ModifiedByNodeId", model.ModifiedByNodeId);
                        result= App.SQLClient.ExecuteNonQuery(command, rollback);
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
        }
    }
}
