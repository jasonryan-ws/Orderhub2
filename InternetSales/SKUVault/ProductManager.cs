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
    public static class ProductManager
    {
        // Get Product by SKU/Code
        public async static Task<Product> Get(string identifier)
        {
            try
            {
                var request = new GetProductRequest();
                var token = API.Token;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                request.ProductSKU = identifier;
                request.ProductCode = identifier;
                var response = await Client.POST<GetProductResponse>(URI.POST_GetProduct, request);
                return response.Product;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Get Products by Date Range
        public async static Task<List<Product>> Get(DateTime minDate, DateTime maxDate, int pageNumber = 0, int pageSize = 10000)
        {
            try
            {
                var request = new GetProductsRequest();
                var token = API.Token;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                request.ModifiedAfterDateTimeUtc = minDate;
                request.ModifiedBeforeDateTimeUtc = maxDate;
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;
                var response = await Client.POST<GetProductsResponse>(URI.POST_GetProducts, request);
                return response.Products;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get Products by List of SKUs/Codes
        public async static Task<List<Product>> Get(List<string> skus, int pageNumber = 0, int pageSize = 10000)
        {
            try
            {
                var request = new GetProductsRequest();
                var token = API.Token;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                request.PageNumber = pageNumber;
                request.PageSize = pageSize;
                request.ProductSKUs = skus;
                var response = await Client.POST<GetProductsResponse>(URI.POST_GetProducts, request);
                return response.Products;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
