using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers
{
    public enum TaskType
    {
        [Description("Update Orders")]
        UpdateOrders,
        [Description("DownloadPurchaseOrders")]
        DownloadPurchaseOrders }
    public static class TaskManager
    {
        /// <summary>
        /// Get Task ID by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Guid? Get(string name)
        {
            try
            {
                Guid? id = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"SELECT TOP 1 Id FROM Task WHERE [Name] = @Name";
                    command.Parameters.AddWithValue("@Name", name);
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        id = row["Id"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["Id"])) : null;
                    }
                }
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Guid? GetByTaskType(TaskType type)
        {
            try
            {
                // Convert enum to name
                var name = Utilities.Generic.GetEnumDescription(type);
                return Get(name);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
