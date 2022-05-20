using Newtonsoft.Json;
using SKUVault.Models;
using SKUVault.Models.Requests;
using SKUVault.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault
{
    public static class LocationManager
    {

        /// <summary>
        /// Get locations by a single sku
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>List of locations</returns>
        public static async Task<List<Location>> GetBySKU(string sku, int pageNumber = 0, int pageSize = 10000)
        {
            try
            {
                var list = new List<string> { sku };
                var results = await GetBySKUs(list, pageNumber, pageSize);
                return results.GetValueOrDefault(sku);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Get inventory by list of SKUs
        /// </summary>
        /// <param name="skus"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Dictionary<SKU-as-Key, List of location></returns>
        public async static Task<ItemWrapper> GetBySKUs(List<string> skus, int pageNumber = 0, int pageSize = 10000)
        {
            try
            {
                var token = API.Token;
                var request = new GetInventoryByLocationRequest();
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;
                request.ProductSKUs = skus;
                var response = await Client.POST(URI.POST_GetInventoryByLocation, request);
                var content = response.Content;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var results = JsonConvert.DeserializeObject<GetInventoryByLocationResponse>(content);
                    return results.Items;
                }
                throw new Exception(content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns all the existing locations in SKUVault
        /// Does not return the inventory - sucks
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Location>> Get()
        {
            try
            {
                var token = API.Token;
                var request = new GenericRequest();
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                var response = await Client.POST<GetLocationsResponse>(URI.POST_GetLocations, request);
                return response.Items;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
