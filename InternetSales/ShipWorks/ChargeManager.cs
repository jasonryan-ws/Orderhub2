using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.Data.SqlClient;
using ShipWorks.Models;

namespace ShipWorks
{
    public static class ChargeManager
    {
        /// <summary>
        /// Load order charges asynchronously by order ID
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static async Task<List<Charge>> LoadByOrderIdAsync(int orderId)
        {
            try
            {
                var models = new List<Charge>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM OrderCharge WHERE OrderId = @OrderId";
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        var table = Configuration.Client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new Charge();
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

        private static void Fill(Charge model, DataRow row)
        {
            model.Id = Convert.ToInt32(row["OrderChargeId"]);
            model.OrderId = Convert.ToInt32(row["OrderId"]);
            model.Type = Convert.ToString(row["Type"]);
            model.Description = Convert.ToString(row["Description"]);
            model.Amount = Convert.ToDecimal(row["Amount"]);
        }
    }
}
