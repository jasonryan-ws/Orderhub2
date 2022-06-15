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
    public static class ChargeManager
    {
        /// <summary>
        /// Get by charge Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ChargeModel> GetAsync(Guid id)
        {
            try
            {
                ChargeModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Charge WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", id);
                        var table= App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        { 
                            model = new ChargeModel();
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
        /// Get by charge name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<ChargeModel> GetAsync(string name)
        {
            try
            {
                ChargeModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Charge WHERE Name = @Name";
                        command.Parameters.AddWithValue("@Name", name);
                        var table= App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new ChargeModel();
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
        /// Get all existing charges
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ChargeModel>> GetAsync()
        {
            try
            {
                var models = new List<ChargeModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Charge";
                        var table= App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new ChargeModel();
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

        public static int Create(ChargeModel model, bool? forceUpdate = null, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spCharge_Create
                        @Id OUTPUT,
                        @Name,
                        @Description,
                        @CreatedByNodeId,
                        @ForceUpdate";

                    var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@Description", !string.IsNullOrWhiteSpace(model.Description) ? model.Description : DBNull.Value);
                    if (model.CreatedByNodeId == Guid.Empty)
                        model.CreatedByNodeId = NodeManager.ActiveNode.Id;
                    command.Parameters.AddWithValue("@CreatedByNodeId", model.CreatedByNodeId);
                    command.Parameters.AddWithValue("@ForceUpdate", forceUpdate != null ? forceUpdate : DBNull.Value);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);

                    model.Id = (Guid)id.Value;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        


        private static void Fill(ChargeModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Description = Convert.ToString(row["Description"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["Id"]));
            model.DateModified = row["DateModified"] != DBNull.Value ? Convert.ToDateTime(row["DateCreated"]) : null;
            model.ModifiedByNodeId = row["ModifiedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["ModifiedByNodeId"])) : null;
        }
    }
}
