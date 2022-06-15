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
        public static JobModel GetActive()
        {
            try
            {
                JobModel model = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"
                            SELECT TOP 1
	                            t.Name,
	                            t.Description,
	                            j.*                                
                            FROM Job j
                            JOIN Task t ON t.Id = j.TaskId
                            WHERE
	                            (IsFinished = 0 OR IsFinished IS NULL) AND
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

        /// <summary>
        /// Start - Creates a new job and mark as started
        /// </summary>
        /// <param name="model"></param>
        /// <param name="taskType"></param>
        /// <param name="forceStart"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        public static int Start(JobModel model, TaskType? taskType = null, bool forceStart = false, bool rollback = false)
        {
            try
            {
                Exception exception = null;
                var result = 0;
                // Override model.TaskId if taskType is set
                if (taskType != null)
                {
                    var taskId = TaskManager.GetByTaskType((TaskType)taskType);
                    if (taskId != null)
                        model.TaskId = (Guid)taskId;
                }

                // Set model.StartedByNodeId if null or empty
                if (model.StartedByNodeId == Guid.Empty)
                {
                    model.StartedbyNode = NodeManager.ActiveNode;
                    model.StartedByNodeId = model.StartedbyNode.Id;

                }
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spJob_Start
                        @Id OUTPUT,
                        @TaskId,
                        @StartedByNodeId,
                        @MaxCount,
                        @ForceStart";
                    var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    command.Parameters.AddWithValue("@TaskId", model.TaskId);
                    command.Parameters.AddWithValue("@StartedByNodeId", model.StartedByNodeId);
                    command.Parameters.AddWithValue("@MaxCount", model.MaxCount);
                    command.Parameters.AddWithValue("@ForceStart", forceStart);
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

        public static int End(JobModel model, bool cancelled = false, bool rollback = false)
        {
            try
            {
                var result = 0;


                // Set model.StartedByNodeId if null or empty
                if (model.EndedByNodeId == null)
                {
                    model.EndedByNode = NodeManager.ActiveNode;
                    model.EndedByNodeId = model.EndedByNode.Id;
                }

                model.IsFinished = !cancelled;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                    @"EXEC spJob_End
                        @Id,
                        @EndedByNodeId,
                        @Message,
                        @IsFinished";
                    command.Parameters.AddWithValue("@Id", model.Id);
                    command.Parameters.AddWithValue("@EndedByNodeId", model.EndedByNodeId);
                    command.Parameters.AddWithValue("@Message", model.Message);
                    command.Parameters.AddWithValue("@IsFinished", model.IsFinished);
                    result = App.SqlClient.ExecuteNonQuery(command, rollback);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Cancel(JobModel model, bool rollback = false)
        {
            try
            {
                model.IsFinished = false;
                return End(model, rollback);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Set the job progression
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="message"></param>
        /// <returns>Returns the current progress between 0 and 100 (%)</returns>
        public static int SetProgression(JobModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"EXEC @Progress = spJob_SetProgression @Id, @Count, @Message, @DateEnded OUTPUT, @EndedByNodeId OUTPUT";
                    var progress = new SqlParameter("@Progress", SqlDbType.Int);
                    progress.Direction = ParameterDirection.Output;

                    var dateEnded = new SqlParameter("@DateEnded", SqlDbType.DateTime);
                    dateEnded.Direction = ParameterDirection.Output;

                    var endedByNodeId = new SqlParameter("@EndedByNodeId", SqlDbType.UniqueIdentifier);
                    endedByNodeId.Direction = ParameterDirection.Output;

                    command.Parameters.Add(progress);
                    command.Parameters.Add(dateEnded);
                    command.Parameters.Add(endedByNodeId);
                    command.Parameters.AddWithValue("@Id", model.Id);
                    command.Parameters.AddWithValue("@Count", model.Count);
                    command.Parameters.AddWithValue("@Message", model.Message);
                    App.SqlClient.ExecuteNonQuery(command, rollback);
                    result = (int)progress.Value;

                    model.DateEnded = dateEnded.Value != DBNull.Value ? (DateTime)dateEnded.Value : null;
                    model.EndedByNodeId = endedByNodeId.Value != DBNull.Value ? (Guid)endedByNodeId.Value : null;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
            if (row["Count"] != DBNull.Value)
                model.Count = Convert.ToInt32(row["Count"]);
            if (row["MaxCount"] != DBNull.Value)
                model.MaxCount = Convert.ToInt32(row["MaxCount"]);
            model.Message = Convert.ToString(row["Message"]);
            if (row["DateProgressionSet"] != DBNull.Value)
                model.DateProgressionSet = Convert.ToDateTime(row["DateProgressionSet"]);
            model.IsFinished = Convert.ToBoolean(row["IsFinished"]);
            if (row["DateEnded"] != DBNull.Value)
                model.DateEnded = Convert.ToDateTime(row["DateEnded"]);
            if (row["EndedByNodeId"] != DBNull.Value)
                model.EndedByNodeId = Guid.Parse(Convert.ToString(row["EndedByNodeId"]));
        }
    }
}
