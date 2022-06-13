using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipWorks
{
    /// <summary>
    /// ShipWorks database configurations
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Initialize client with full database crendentials
        /// </summary>
        public static void Initialize(int storeId, string server, string userId, string password, string database)
        {
            StoreId = storeId;
            Server = server;
            UserId = userId;
            Password = password;
            Database = database;

        }

        /// <summary>
        /// Initialize client with your Windows login
        /// </summary>
        public static void Initialize(int storeId, string server, string database)
        {
            StoreId = storeId;
            Server = server;
            Database = database;
        }

        public static int StoreId { get; set; }
        public static string Server { get; set; }
        public static string UserId { get; set; }
        public static string Password { get; set; }
        public static string Database { get; set; }
        public static bool IsIntegrated { get; set; } // Set to true if you want to use your Windows login

        public static SQL Client
        {                                                                                                                                                                                                            
            get
            {
                var connectionString = Utilities.Builder.SQLConnectionString(Server, Database, IsIntegrated, UserId, Password);
                return new SQL(connectionString);
            }
        }

        // Use only for testing this app
        public static void TestDatabaseCredentials()
        {
            Initialize(23005, "192.168.22.12", "sa", "1bike2work", "ShipWorks");
        }
    }
}
