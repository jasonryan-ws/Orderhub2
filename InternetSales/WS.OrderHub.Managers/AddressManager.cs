using Data;
using Microsoft.Data.SqlClient;
using System.Data;
using WS.OrderHub.Models;
namespace WS.OrderHub.Managers
{
    public static class AddressManager
    {
        static readonly SQL client = LocalConfigurationManager.SQLClient();
        public static async Task<List<AddressModel>> GetAsync()
        {
            try
            {
                var models = new List<AddressModel>();
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "SELECT * FROM Address";
                        var table = client.ExecuteQuery(command);
                        foreach (DataRow row in table.Rows)
                        {
                            var model = new AddressModel();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rollback">Set to true if you don't want to commit the changes in the database</param>
        /// <returns></returns>
        public static async Task<int> CreateAsync(AddressModel model, bool rollback = false)
        {
            try
            {
                var result = 0;
                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        $@"EXEC spAddress_Create
                            @Id OUTPUT,
                            @FirstName,
                            @MiddleName,
                            @LastName,
                            @Company,
                            @Street1,
                            @Street2,
                            @Street3,
                            @City,
                            @State,
                            @PostalCode,
                            @CountryCode,
                            @Phone,
                            @Fax,
                            @Email,
                            @CreatedByNodeId";
                        var id = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                        id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(id);
                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@MiddleName", model.MiddleName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Company", model.Company);
                        command.Parameters.AddWithValue("@Street1", model.Street1);
                        command.Parameters.AddWithValue("@Street2", model.Street2);
                        command.Parameters.AddWithValue("@Street3", model.Street3);
                        command.Parameters.AddWithValue("@City", model.City);
                        command.Parameters.AddWithValue("@State", model.State);
                        command.Parameters.AddWithValue("@PostalCode", model.PostalCode);
                        command.Parameters.AddWithValue("@CountryCode", model.CountryCode);
                        command.Parameters.AddWithValue("@Phone", model.Phone);
                        command.Parameters.AddWithValue("@Fax", model.Fax);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@CreatedByNodeId", model.CreatedByNodeId);
                        result = client.ExecuteNonQuery(command, rollback);
                        model.Id = (Guid)id.Value;
                    }

                });

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update address
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rollback">Only set this to true if running a test</param>
        /// <returns>Number of affected rows</returns>
        public static async Task<int> UpdateAsync(AddressModel model, bool rollback = false)
        {
            try
            {
                var result = 0;

                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText =
                        $@"EXEC spAddress_Update
                         @Id,
                         @FirstName,
                         @MiddleName,
                         @LastName,
                         @Company,
                         @Street1,
                         @Street2,
                         @Street3,
                         @City,
                         @State,
                         @PostalCode,
                         @CountryCode,
                         @Phone,
                         @Fax,
                         @Email,
                         @ModifiedByNodeId";
                        command.Parameters.AddWithValue("@Id", model.Id);
                        command.Parameters.AddWithValue("@FirstName", model.FirstName);
                        command.Parameters.AddWithValue("@MiddleName", model.MiddleName);
                        command.Parameters.AddWithValue("@LastName", model.LastName);
                        command.Parameters.AddWithValue("@Company", model.Company);
                        command.Parameters.AddWithValue("@Street1", model.Street1);
                        command.Parameters.AddWithValue("@Street2", model.Street2);
                        command.Parameters.AddWithValue("@Street3", model.Street3);
                        command.Parameters.AddWithValue("@City", model.City);
                        command.Parameters.AddWithValue("@State", model.State);
                        command.Parameters.AddWithValue("@PostalCode", model.PostalCode);
                        command.Parameters.AddWithValue("@CountryCode", model.CountryCode);
                        command.Parameters.AddWithValue("@Phone", model.Phone);
                        command.Parameters.AddWithValue("@Fax", model.Fax);
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@ModifiedByNodeId", model.ModifiedByNodeId);
                        result = client.ExecuteNonQuery(command, rollback);

                    }
                });

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete addresses that are not tied to any orders or stores
        /// </summary>
        /// <param name="rollback"></param>
        /// <returns></returns>
        public static async Task<int> DeleteUnusedAsync(bool rollback = false)
        {
            try
            {
                var result = 0;

                await Task.Run(() =>
                {
                    using (var command = new SqlCommand())
                    {
                        command.CommandText = "EXEC spAddress_DeleteUnused";
                        result = client.ExecuteNonQuery(command, rollback);

                    }
                });

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static void Fill(AddressModel model, DataRow row)
        {
            model.Id = Guid.Parse(Convert.ToString(row["Id"]));
            model.FirstName = Convert.ToString(row["FirstName"]);
            model.MiddleName = Convert.ToString(row["MiddleName"]);
            model.LastName = Convert.ToString(row["LastName"]);
            model.Company = Convert.ToString(row["Company"]);
            model.Street1 = Convert.ToString(row["Street1"]);
            model.Street2 = Convert.ToString(row["Street2"]);
            model.Street3 = Convert.ToString(row["Street3"]);
            model.City = Convert.ToString(row["City"]);
            model.State = Convert.ToString(row["State"]);
            model.PostalCode = Convert.ToString(row["PostalCode"]);
            model.CountryCode = Convert.ToString(row["CountryCode"]);
            model.Phone = Convert.ToString(row["Phone"]);
            model.Fax = Convert.ToString(row["Fax"]);
            model.Email = Convert.ToString(row["Email"]);
            model.DateCreated = Convert.ToDateTime(row["DateCreated"]);
            model.CreatedByNodeId = Guid.Parse(Convert.ToString(row["CreatedByNodeId"]));
            model.DateModified = row["DateModified"] != DBNull.Value ? Convert.ToDateTime(row["DateModified"]) : null;
            model.ModifiedByNodeId = row["ModifiedByNodeId"] != DBNull.Value ? Guid.Parse(Convert.ToString(row["ModifiedByNodeId"])) : null;
        }
    }
}
