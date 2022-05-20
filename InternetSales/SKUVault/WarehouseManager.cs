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
    public static class WarehouseManager
    {
        public async static Task<List<Warehouse>> Get(int pageNumber = 0)
        {
            try
            {
                var token = API.Token;
                var request = new GenericRequest();
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                request.PageNumber = pageNumber;
                var response = await Client.POST<GetWarehousesReponse>(URI.POST_GetWarehouses, request);
                return response.Warehouses;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
