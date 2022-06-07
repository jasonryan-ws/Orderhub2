using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO = Utilities.FileIO;
using Cypher = Utilities.Cryptography;
namespace WS.OrderHub.Managers
{
    public static class App
    {
        private static readonly string PublicKey = "0pBa{WTu";
        private static readonly string PrivateKey = "Rd548#/@";
        private static readonly string mainPath = Environment.CurrentDirectory + @"\config\";
        private static readonly string connectionPath = mainPath + "connection.cfg";
        private static readonly string printerPath = Environment.CurrentDirectory + "printer.cfg";

        public static SQL SQLClient { get => IntializeClient(); }

        private static SQL IntializeClient()
        {
            try
            {
                SQL client = null;
                var connectionString = FileIO.Text.GetValueFromTextFile(connectionPath, "ConnectionString");

                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    client = new SQL(connectionString);
                }
                else
                {
                    var server = FileIO.Text.GetValueFromTextFile(connectionPath, "Server");
                    var isIntegrated = FileIO.Text.GetValueFromTextFile(connectionPath, "IsIntegrated").ToUpper() == "TRUE";
                    var database = FileIO.Text.GetValueFromTextFile(connectionPath, "Database");

                    if (isIntegrated)
                    {
                        client = new SQL(server, database);
                    }
                    else
                    {

                        var userId = FileIO.Text.GetValueFromTextFile(connectionPath, "UserId");
                        var password = FileIO.Text.GetValueFromTextFile(connectionPath, "Password");
                        var isEncrypted = FileIO.Text.GetValueFromTextFile(connectionPath, "IsEncrypted").ToUpper() == "TRUE";
                        if (isEncrypted)
                            password = Cypher.AES.Decrypt(password, PublicKey, PrivateKey);
                        client = new SQL(server, database, userId, password);
                    }
                }

                return client;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
