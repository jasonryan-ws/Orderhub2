using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class SQL
    {
        public string ConnectionString { get; set; }


        public SQL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int ExecuteNonQuery(SqlCommand command, bool rollback = false)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    command.Transaction = connection.BeginTransaction();

                    var rowCount = command.ExecuteNonQuery();

                    if (rollback)
                        command.Transaction.Rollback();
                    else
                        command.Transaction.Commit();
                    return rowCount;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataTable ExecuteQuery(SqlCommand command)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    command.Connection = connection;
                    command.Connection.Open();
                    var da = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<string> GetDatabases(string connectionString)
        {
            try
            {
                var names = new List<string>();
                var sql = new SQL(connectionString);
                var command = new SqlCommand();
                command.CommandText = "sp_databases";
                var table = sql.ExecuteQuery(command);

                foreach (DataRow r in table.Rows)
                {
                    names.Add(r["DATABASE_NAME"].ToString());
                }
                return names;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TestOK()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch { return false; }

        }
    }
}
