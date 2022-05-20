using Newtonsoft.Json;
using RestSharp;
using SKUVault.Models.Responses;

namespace SKUVault
{
    /// <summary>
    /// Generic RestClient API calls
    /// </summary>
    public static class Client
    {
        public async static Task<RestResponse> POST(string requestURI, object requestBody)
        {
            try
            {
                var client = new RestClient(requestURI);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(requestBody);
                return await client.PostAsync(request);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<T?> POST<T>(string requestURI, object requestBody)
        {
            try
            {
                var client = new RestClient(requestURI);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(requestBody);
                return await client.PostAsync<T>(request);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static RestRequest Request(object body)
        {
            try
            {
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(body);
                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}