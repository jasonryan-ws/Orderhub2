using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.Managers
{
    public static class LineSessionManager
    {
        /// <summary>
        /// Checks-in order item
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="checkingInQty"></param>
        /// <param name="feedback"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        public static int? CheckIn(Guid orderId, Guid productId, int receivingQty, out string feedback, bool rollback = false)
        {
            try
            {
                int? result = null;
                using (var command = new SqlCommand())
                {
                    command.CommandText =
                        @"EXEC @Output = [spLineSession_CheckInOrderItem]
                        @OrderId,
                        @ProductId,
                        @ReceivingQty,
                        @CreatedByNodeId,
                        @Feedback OUTPUT";

                    var output = new SqlParameter("@Output", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    command.Parameters.Add(output);

                    var message = new SqlParameter("@Feedback", SqlDbType.VarChar, 100);
                    message.Direction = ParameterDirection.Output;
                    command.Parameters.Add(message);

                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@ReceivingQty", receivingQty);
                    command.Parameters.AddWithValue("@CreatedByNodeId", NodeManager.ActiveNode.Id);

                    App.SqlClient.ExecuteNonQuery(command, rollback);

                    result = (int?)output.Value;
                    feedback = (string)message.Value;
                }
                    return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
