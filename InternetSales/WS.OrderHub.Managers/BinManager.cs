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
    public static class BinManager
    {
        private static SQL sql = LocalConfigurationManager.SQLClient();
        /// <summary>
        /// Inserts a new bin into the database
        /// </summary>
        /// <param name="model">BinModel</param>
        /// <param name="forceUpdate">If name exists and forceUpdate is null - throws error, false - hide error, true - perform update)</param>
        /// <param name="rollback">If true, changes will not be committed. For UnitTesting only.</param>
        /// <returns></returns>
        public static async Task<int> CreateAsync(BinModel model, bool? forceUpdate = null, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                            @"EXEC spBin_Create
                            @Id OUTPUT,
                            @Name,
                            @IsReserved,
                            @IsDefault,
                            @CreatedByNodeId,
                            @ForceUpdate";
                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@IsReserved", model.IsReserved);
                        command.Parameters.AddWithValue("@IsDefault", model.IsDefault);
                        if (model.CreatedByNodeId == Guid.Empty)
                            model.CreatedByNodeId = NodeManager.NodeId;
                        command.Parameters.AddWithValue("@CreatedByNodeId", model.CreatedByNodeId);
                        command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                        result = sql.ExecuteNonQuery(command, rollback);
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

        public static async Task<int> UpdateAsync(BinModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                            @"EXEC spBin_Update
                            @Id,
                            @Name,
                            @IsReserved,
                            @IsDefault,
                            @ModifiedByNodeId";
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@IsReserved", model.IsReserved);
                        command.Parameters.AddWithValue("@IsDefault", model.IsDefault);
                        if (model.ModifiedByNodeId == null)
                            model.ModifiedByNodeId = NodeManager.NodeId;
                        command.Parameters.AddWithValue("@ModifiedByNodeId", model.ModifiedByNodeId);
                        result = sql.ExecuteNonQuery(command, rollback);
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get bin location by id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<BinModel> GetAsync(Guid id)
        {
            try
            {
                BinModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"SELECT * FROM Bin WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", id);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new BinModel();
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
        /// Get bin location by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<BinModel> GetAsync(string name)
        {
            try
            {
                BinModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"SELECT * FROM Bin WHERE Name = @Name";
                        command.Parameters.AddWithValue("@Name", name);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new BinModel();
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
        /// Get all bin locations
        /// </summary>
        /// <returns></returns>
        public static async Task<List<BinModel>> GetAsync()
        {
            try
            {
                var models = new List<BinModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"SELECT * FROM Bin";
                        var table = sql.ExecuteQuery(command);
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
        }
    }
}
