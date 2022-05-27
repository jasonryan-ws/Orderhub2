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
        public SQL(string server, string initialCatalog, string userId, string password)
        {
            ConnectionString =
                $"Data Source={server};" +
                $"TrustServerCertificate=True;" +
                $"Initial Catalog={initialCatalog};" +
                $"User ID={userId};" +
                $"Password={password}";
        }

        public SQL(string server, string initialCatalog)
        {
            ConnectionString =
                $"Server={server};" +
                $"TrustServerCertificate=True;" +
                $"Database={initialCatalog};" +
                $"Integrated Security=True;";
        }

        public SQL(string server, string userId, string password)
        {
            ConnectionString =
                $"Server={server};" +
                $"TrustServerCertificate=True;" +
                $"UID={userId};" +
                $"Pwd={password}";
        }

        public SQL(string server, bool isIntegrated)
        {
            ConnectionString =
                $"Data Source={server};" +
                $"Integrated Security={isIntegrated};";
        }


        // For non SELECT statements (DELETE, UPDATE, INSERT, etc...)
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

        // For SELECT statements
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

        public static List<string> GetDatabases(SQL sql)
        {
            try
            {
                var names = new List<string>();
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

        // Get databases by connection string
        public static List<string> GetDatabases(string connectionString)
        {
            try
            {
                var sql = new SQL(connectionString);
                return GetDatabases(sql);
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