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
    public static class JobManager
    {
        // Get
        public static async Task<JobModel> Get(Guid id)
        {
            JobModel model = null;
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"
                    SELECT
	                    t.Name,
	                    t.Description,
	                    j.*
                    FROM Job j
                    JOIN Task t ON t.Id = j.TaskId
                    WHERE j.Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new JobModel();
                        Fill(model, row);
                    }
                }
            });

            return model;
        }

        // Get
        public static async Task<List<JobModel>> GetByTaskId(Guid taskId)
        {
            var models = new List<JobModel>();
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"
                    SELECT
	                    t.Name,
	                    t.Description,
	                    j.*
                    FROM Job j
                    JOIN Task t ON t.Id = j.TaskId
                    WHERE t.Id = @TaskId";
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        var model = new JobModel();
                        Fill(model, row);
                        models.Add(model);
                    }
                }
            });

            return models;
        }

        /// <summary>
        /// Get the latest active job
        /// </summary>
        /// <returns></returns>
        public static async Task<JobModel> GetActiveAsync()
        {
            try
            {
                JobModel model = null;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"
                            SELECT TOP 1 * 
                            FROM Job
                            WHERE
	                            IsFinished = 0 AND
	                            DateEnded IS NULL
                            ORDER BY
	                            DateStarted DESC";
                        var table = App.SqlClient.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            model = new JobModel();
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

        public static async Task<List<JobModel>> Get()
        {
            var models = new List<JobModel>();
            await Task.Run(() =>
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"
                    SELECT
	                    t.Name,
	                    t.Description,
	                    j.*
                    FROM Job j
                    JOIN Task t ON t.Id = j.TaskId";
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        var model = new JobModel();
                        Fill(model, row);
                        models.Add(model);
                    }
                }
            });

            return models;
        }

        // Start - Creates a new job and mark as started
        public static async Task<int> StartAsync(JobModel model, bool forceStart = false, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        @"EXEC spJob_Start
                        @Id OUTPUT,
                        @TaskId,
                        @StartedByNodeId,
                        @ForceStart";
                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@TaskId", model.TaskId);
                        command.Parameters.AddWithValue("@StartedByNodeId", model.StartedByNodeId);
                        command.Parameters.AddWithValue("@ForceStart", forceStart);
                        result = App.SqlClient.ExecuteNonQuery(command, rollback);
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

        // Progress


        // End


        private static void Fill(JobModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.TaskId = Guid.Parse(Convert.ToString(row["TaskId"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Description = Convert.ToString(row["Description"]);

            if (row["DateStarted"] != DBNull.Value)
                model.DateStarted = Convert.ToDateTime(row["DateStarted"]);
            if (row["StartedByNodeId"] != DBNull.Value)
                model.StartedByNodeId = Guid.Parse(Convert.ToString(row["StartedByNodeId"]));
            if (row["Progress"] != DBNull.Value)
                model.Progress = Convert.ToInt32(row["Progress"]);
            model.Message = Convert.ToString(row["Message"]);
            if (row["DateProgressed"] != DBNull.Value)
                model.DateProgressed = Convert.ToDateTime(row["DateProgressed"]);
            if (row["IsFinished"] != DBNull.Value)
                model.IsFinished = Convert.ToBoolean(row["IsFinished"]);
            if (row["DateEnded"] != DBNull.Value)
                model.DateEnded = Convert.ToDateTime(row["DateEnded"]);
            if (row["EndedByNodeId"] != DBNull.Value)
                model.EndedByNodeId = Guid.Parse(Convert.ToString(row["EndedByNodeId"]));
        }
    }
}
