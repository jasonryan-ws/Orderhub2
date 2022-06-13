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
        /// <summary>
        /// Get channel by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChannelModel</returns>
        public static async Task<ChannelModel> GetAsync(Guid id)
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
                        var table= App.SqlClient.ExecuteQuery(command);
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
        public static async Task<ChannelModel> GetAsync(string name)
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
                        var table= App.SqlClient.ExecuteQuery(command);
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
        public static async Task<List<ChannelModel>> GetAsync()
        {
            try
            {
                var models = new List<ChannelModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Channel";
                        var table= App.SqlClient.ExecuteQuery(command);
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
        /// <summary>
        /// Create a new channel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        public static async Task<int> CreateAsync(ChannelModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spChannel_Create
                        @Id OUTPUT,
                        @Name,
                        @Code,
                        @ColorCode,
                        @CreatedByNodeId";
                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("Name", model.Name);
                        command.Parameters.AddWithValue("Code", model.Code);
                        command.Parameters.AddWithValue("ColorCode", model.ColorCode);
                        if (model.CreatedByNodeId == Guid.Empty)
                            model.CreatedByNodeId = NodeManager.NodeId;
                        command.Parameters.AddWithValue("@CreatedByNodeId", model.CreatedByNodeId);
                        result= App.SqlClient.ExecuteNonQuery(command, rollback);
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

        /// <summary>
        /// Update channel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync(ChannelModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spChannel_Update
                        @Id,
                        @Name,
                        @Code,
                        @ColorCode,
                        @ModifiedByNodeId";
                        command.Parameters.AddWithValue("Id", model.Id);
                        command.Parameters.AddWithValue("Name", model.Name);
                        command.Parameters.AddWithValue("Code", model.Code);
                        command.Parameters.AddWithValue("ColorCode", model.ColorCode != null ? model.ColorCode : DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedByNodeId", model.ModifiedByNodeId != null ? model.ModifiedByNodeId : NodeManager.NodeId);
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

        private static void Fill(ChannelModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Code = Convert.ToString(row["Code"]);
            model.ColorCode = row["ColorCode"] != DBNull.Value ? Convert.ToInt32(row["ColorCode"]) : null;
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
