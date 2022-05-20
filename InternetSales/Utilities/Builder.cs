using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Builder
    {
        /// <summary>
        /// /// Requires full database credentials
        /// </summary>
        /// <param name="server"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string SQLConnectionString(string server, string initialCatalog, string userId, string password)
        {
            return
                $"Data Source={server};" +
                $"TrustServerCertificate=True;" +
                $"Initial Catalog={initialCatalog};" +
                $"User ID={userId};" +
                $"Password={password}";
        }

        /// <summary>
        /// /// Requires full database credentials if isIntegrated is set to false
        /// </summary>
        /// <param name="server"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string SQLConnectionString(string server, string initialCatalog, bool isIntegrated, string userId = null, string password = null)
        {
            if (isIntegrated)
                return SQLConnectionString(server, initialCatalog);
            else
                return
                    $"Data Source={server};" +
                    $"TrustServerCertificate=True;" +
                    $"Initial Catalog={initialCatalog};" +
                    $"User ID={userId};" +
                    $"Password={password}";
        }


        /// <summary>
        ///  Windows login will be used for authentication
        /// </summary>
        /// <param name="server"></param>
        /// <param name="initialCatalog"></param>
        /// <returns></returns>
        public static string SQLConnectionString(string server, string initialCatalog)
        {
            return
                $"Server={server};" +
                $"TrustServerCertificate=True;" +
                $"Database={initialCatalog};" +
                $"Integrated Security=True;";
        }

        /// <summary>
        /// Requires full database credentials
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string SQLConnectionString(string server, string userId, string password)
        {
            return
                $"Server={server};" +
                $"TrustServerCertificate=True;" +
                $"UID={userId};" +
                $"Pwd={password}";
        }

        /// <summary>
        /// Windows login will be used for authentication.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string SQLConnectionString(string server)
        {
            return
                $"Data Source={server};" +
                $"Integrated Security=True;";
        }




    }
}
