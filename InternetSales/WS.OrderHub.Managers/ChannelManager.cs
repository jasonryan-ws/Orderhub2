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
    public class ChannelManager
    {
        private static SQL sql = LocalConfigurationManager.SQLClient();

        /// <summary>
        /// Get channel by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChannelModel</returns>
        public static async Task<ChannelModel> Get(Guid id)
        {
            try
            {
                ChannelModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Channel WHERE Id = @Id";
                        command.Parameters.AddWithValue("Id", id);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new ChannelModel();
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
        /// Get channel by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>ChannelModel</returns>
        public static async Task<ChannelModel> Get(string name)
        {
            try
            {
                ChannelModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Channel WHERE Name = @Name";
                        command.Parameters.AddWithValue("Name", name);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new ChannelModel();
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
        /// Get all channels
        /// </summary>
        /// <returns>List of ChannelModels</returns>
        public static async Task<List<ChannelModel>> Get()
        {
            try
            {
                var models = new List<ChannelModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Channel";
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new ChannelModel();
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



        private static void Fill(ChannelModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Code = Convert.ToString(row["Code"]);
            model.ColorCode = Convert.ToString(row["ColorCode"]);
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
