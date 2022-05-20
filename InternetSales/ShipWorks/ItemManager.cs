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
    public static class ItemManager
    {
        public static async Task<List<Item>> LoadByOrderIdAsync(int orderId)
        {
            try
            {
                var models = new List<Item>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"
                        SELECT
	                        SKU,
	                        UPC,
	                        [Name],
	                        [Location],
	                        [Image] as 'ImageURL',
	                        Thumbnail as 'ThumbnailURL',
	                        UnitPrice as 'Price',
	                        SUM(Quantity) as 'Quantity'
                        FROM OrderItem i
                        JOIN [Order] o ON o.OrderID = i.OrderID
                        WHERE
	                        o.OrderId = @OrderId
                        GROUP BY
	                        SKU,
	                        UPC,
	                        [Name],
	                        [Location],
	                        [Image],
	                        Thumbnail,
	                        UnitPrice";
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        var table = Configuration.Client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new Item();
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

        private static void Fill(Item model, DataRow row)
        {
            model.SKU = Convert.ToString(row["SKU"]);
            model.UPC = Convert.ToString(row["UPC"]);
            model.Name = Convert.ToString(row["Name"]);
            model.Location = Convert.ToString(row["Location"]);
            model.ImageURL = Convert.ToString(row["ImageURL"]);
            model.ThumbnailURL = Convert.ToString(row["ThumbnailURL"]);
            model.Price = Convert.ToDecimal(row["Price"]);
            model.Quantity = Convert.ToInt32(row["Quantity"]);
        }
    }
}
