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
    public static class SupplierManager
    {
        // Get list of all existing suppliers
        public async static Task<List<Supplier>> Get(int pageNumber = 0)
        {
            try
            {
                var token = API.Token;
                var request = new GenericRequest();
                request.PageNumber = pageNumber;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                var response = await Client.POST<GetSuppliersResponse>(URI.POST_GetSuppliers, request);
                return response.Suppliers;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
