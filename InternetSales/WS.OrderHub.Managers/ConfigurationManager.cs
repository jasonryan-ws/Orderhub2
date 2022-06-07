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
    public static class ConfigurationManager
    {
        /// <summary>
        /// Get by Configuration Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ConfigurationModel> GetAsync(Guid id)
        {
            ConfigurationModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Configuration WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    var table= App.SQLClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new ConfigurationModel();
                        Fill(model, row);
                    }
                }
            });
            return model;
        }

        /// <summary>
        /// Get by Configuration Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<ConfigurationModel> GetAsync(string name)
        {
            ConfigurationModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Configuration WHERE Name = @Name";
                    command.Parameters.AddWithValue("@Name", name);
                    var table= App.SQLClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new ConfigurationModel();
                        Fill(model, row);
                    }
                }
            });
            return model;
        }

        /// <summary>
        /// Get all configurations
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ConfigurationModel>> GetAsync()
        {
            var models = new List<ConfigurationModel>();
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM Configuration";
                    var table= App.SQLClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        var model = new ConfigurationModel();
                        Fill(model, row);
                        models.Add(model);
                    }
                }
            });
            return models;
        }

        /// <summary>
        /// Get value by name
        /// </summary>
        /// <returns></returns>
        public static async Task<object> GetValue(string name)
        {
            object value = null;
            await Task.Run(async () =>
            {
                var model = await GetAsync(name);
                value = model.Value;
            });
            return value;
        }

        /// <summary>
        /// Update configuration by filled ConfigurationModel
        /// </summary>
        /// <returns></returns>
        public static async Task<int> UpdateAsync(ConfigurationModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                if (model.Id == Guid.Empty && !string.IsNullOrEmpty(model.Name))
                    model.Id = (await GetAsync(model.Name)).Id;
                await Task.Run(() =>
                {

                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spConfiguration_Update
                        @Id,
                        @Value,
                        @ModifiedByNodeId";
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@Value", model.Value);
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

        /// <summary>
        /// Set configuration value by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync(string name, object value, Guid? modifiedByNodeId = null, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    var model = new ConfigurationModel();
                    model.Name = name;
                    model.Value = value;
                    model.ModifiedByNodeId = modifiedByNodeId != null ? modifiedByNodeId : NodeManager.NodeId;
                    result = await UpdateAsync(model, rollback);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Fill(ConfigurationModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Value = row["Value"];
            model.Description = Convert.ToString(row["Description"]);
            model.FullDescription = Convert.ToString(row["FullDescription"]);
            model.DateModified = row["DateModified"] != DBNull.Value ? Convert.ToDateTime(row["DateModified"]) : null;
            model.ModifiedByNodeId = row["ModifiedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["ModifiedByNodeId"])) : null;
        }
    }
}
