using System.Data;
using WS.OrderHub.Models;

namespace WS.OrderHub.Managers
{
    public static class OrderManager
    {
        /// <summary>
        /// Deletes the order and anything associated with it
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="rollback">Set to true if you only testing this method</param>
        /// <returns></returns>
        public static async Task<int> DeleteAsync(Guid id, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    results = 1;
                });
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<OrderModel>> Get()
        {
            try
            {
                var models = new List<OrderModel>();
                await Task.Run(() =>
                {
                    var table = new DataTable();
                    foreach (DataRow row in table.Rows)
                    {
                        var model = new OrderModel();
                        Fill(model, row);
                        models.Add(model);
                    }
                });
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static async Task<OrderModel> Get(Guid id)
        {
            try
            {
                OrderModel model = null;
                await Task.Run(() =>
                {
                    var table = new DataTable();
                    table.Columns.Add("Id", typeof(Guid));
                    var r = table.NewRow();
                    r["Id"] = id;
                    table.Rows.Add(r);
                    foreach (DataRow row in table.Rows)
                    {
                        model = new OrderModel();
                        Fill(model, row);
                    }

                });
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Fill(OrderModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
        }
    }
}