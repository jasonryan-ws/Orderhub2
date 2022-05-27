using Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers
{
    public static class BinManager
    {
        private static SQL client = LocalConfigurationManager.SQLClient();
        public static async Task<int> CreateAsync()
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                            @"EXEC spBin_Create
                            @Id OUTPUT,
                            @Name,
                            @IsReserved,
                            @IsDefault,
                            @CreatedByNodeId";
                    }
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
