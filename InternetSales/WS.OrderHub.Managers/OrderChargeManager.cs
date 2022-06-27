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
    public static class OrderChargeManager
    {


        public static List<ChargeModel> GetByOrderId(Guid orderId, bool hideZeroAmount = false)
        {
            try
            {
                var models = new List<ChargeModel>();
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"
                    SELECT
	                    c.*,
	                    oc.Amount
                    FROM Charge c
                    JOIN OrderCharge oc ON oc.ChargeId = c.Id
                    WHERE
	                    oc.OrderId = @OrderId AND
	                    (oc.Amount > 0 OR @HideZeroAmount = 0)";
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@HideZeroAmount", hideZeroAmount);
                    var table = App.SqlClient.ExecuteQuery(command);
                    foreach (DataRow row in table.Rows)
                    {
                        var model = new ChargeModel();
                        Fill(model, row);
                        models.Add(model);
                    }
                }

                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Guid? Create(Guid orderId, Guid chargeId, decimal amount, bool forceUpdate = false, bool rollback = false)
        {
            try
            {
                Guid? newId = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                        @"EXEC spOrderCharge_Create
                        @Id OUTPUT,
                        @OrderId,
                        @ChargeId,
                        @Amount,
                        @ForceUpdate";
                    var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                    id.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ChargeId", chargeId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@ForceUpdate", forceUpdate);
                    App.SqlClient.ExecuteNonQuery(command, rollback);
                    newId = (Guid)id.Value;
                }

                return newId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void Fill(ChargeModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.Name = Convert.ToString(row["Name"]);
            model.Description = Convert.ToString(row["Description"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["CreatedByNodeId"]));
            if (row["DateModified"] != DBNull.Value)
                model.DateModified = Convert.ToDateTime(row["DateModified"]);
            if (row["ModifiedByNodeId"] != DBNull.Value)
                model.ModifiedByNodeId = Guid.Parse(Convert.ToString(row["ModifiedByNodeId"]));
            model.Amount = Convert.ToDecimal(row["Amount"]);
        }
    }
}
