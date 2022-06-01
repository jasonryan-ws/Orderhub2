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
    public static class NodeManager
    {
        static readonly SQL sql = LocalConfigurationManager.SQLClient();

        public static async Task<int> CreateAsync(NodeModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spNode_Create
                        @Id OUTPUT,
                        @Name,
                        @Description";

                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Description", model.Description);
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


        public static async Task<NodeModel> GetAsync(Guid Id)
        {
            try
            {
                NodeModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM [Node] WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", Id);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new NodeModel();
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
        /// Get Node by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<NodeModel> GetAsync(string name)
        {
            try
            {
                NodeModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM [Node] WHERE Name = @Name";
                        command.Parameters.AddWithValue("@Name", name);
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new NodeModel();
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
        /// Get all existing Nodes
        /// </summary>
        /// <returns></returns>
        public static async Task<List<NodeModel>> GetAsync()
        {
            try
            {
                var models = new List<NodeModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM [Node]";
                        var table = sql.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new NodeModel();
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


        public static async Task<int> DeleteAsync(NodeModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spNode_DeleteUndelete
                        @Id,
                        @IsDeleted,
                        @DateDeleted,
                        @DeletedByNodeId";
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@IsDeleted", model.IsDeleted);
                        command.Parameters.AddWithValue("@DateDeleted", model.DateDeleted);
                        command.Parameters.AddWithValue("@DeletedByNodeId", model.DeletedByNodeId);

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

        private static void Fill(NodeModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Description = Convert.ToString(row["Description"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.DateLastActive = row["DateLastActive"] != DBNull.Value ? Convert.ToDateTime(row["DateLastActive"]) : null;
            model.IsDeleted = Convert.ToBoolean(row["IsDeleted"]);
            model.DateDeleted = row["DateDeleted"] != DBNull.Value ? Convert.ToDateTime(row["DateDeleted"]) : null;
            model.DeletedByNodeId = row["DeletedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["DeletedByNodeId"])) : null;
        }
    }
}
