using Data;
using Microsoft.Data.SqlClient;
using ShipWorks.Models;
using System.Data;

namespace ShipWorks
{
    public static class NoteManager
    {
        public static async Task<List<Note>> LoadByObjectIdAsync(int objectId)
        {
            try
            {
                var models = new List<Note>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = @"
                        SELECT *
                        FROM [Note]
                        WHERE
                            ObjectId = @ObjectId AND
                            Text NOT LIKE '%Client order number:%' AND
                            Text NOT LIKE '%Sales Channel%' AND
                            Text NOT LIKE '%eBay Order Id%'";
                        command.Parameters.AddWithValue("ObjectId", objectId);
                        var table = Configuration.Client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new Note();
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
        private static void Fill(Note model, DataRow row)
        {
            model.Id = Convert.ToInt32(row["NoteId"]);
            model.ObjectId = Convert.ToInt32(row["ObjectId"]);
            model.Text = Convert.ToString(row["Text"]);
        }
    }
}
