using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers
{
    public static class OrderChargeManager
    {
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
    }
}
