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
    public static class PurchaseOrderManager
    {
        /// <summary>
        /// Return list of purchase orders by date range
        /// </summary>
        /// <param name="minDate">Mininum date in UTC</param>
        /// <param name="maxDate">Maximum date in UTC</param>
        /// <param name="includeProducts">Return the products associated to this PO if set to TRUE</param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        //public static List<PurchaseOrder> Get(DateTime minDate, DateTime maxDate, bool includeProducts = true, int pageNumber = 0)
        //{
        //    try
        //    {
        //        var token = API.Token;
        //        var request = new GetPOsRequest();
        //        request.ModifiedAfterDateTimeUtc = minDate;
        //        request.ModifiedBeforeDateTimeUtc = maxDate;
        //        request.IncludeProducts = includeProducts;
        //        request.PageNumber = pageNumber;
        //        request.TenantToken = token.TenantToken;
        //        request.UserToken = token.UserToken;
        //        var response = Client.POST(URI.POST_GetPOs, request);
        //        if (response.IsCompleted)
        //        {
        //            var results = JsonConvert.DeserializeObject<GetPOsResponse>(content);
        //            return results.PurchaseOrders;
        //        }
        //        throw new Exception((content));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// Return list of purchase orders by status
        /// </summary>
        /// <param name="pOStatus">We will return all POs that are not completed by Default. To get completed PO's, you must intentionally specify in the call. Possible values are: NoneReceived, PartiallyReceived, Completed, Cancelled</param>
        /// <param name="includeProducts"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async static Task<List<PurchaseOrder>> Get(POStatus pOStatus = POStatus.NotCompleted, bool includeProducts = true, int pageNumber = 0)
        {
            try
            {
                var token = API.Token;
                var request = new GetPOsRequest();
                if (pOStatus != POStatus.NotCompleted)
                    request.Status = pOStatus;
                request.IncludeProducts = includeProducts;
                request.PageNumber = pageNumber;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                var response = await Client.POST<GetPOsResponse>(URI.POST_GetPOs, request);
                return response.PurchaseOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<PurchaseOrder>> Get(DateTime minDate, DateTime? maxDate = null, POStatus pOStatus = POStatus.NotCompleted, bool includeProducts = true, int pageNumber = 0)
        {
            try
            {
                var token = API.Token;
                var request = new GetPOsRequest();
                if (pOStatus != POStatus.NotCompleted)
                    request.Status = pOStatus;
                request.ModifiedAfterDateTimeUtc = minDate;
                request.ModifiedBeforeDateTimeUtc = maxDate != null ? (DateTime)maxDate : DateTime.Today;
                request.IncludeProducts = includeProducts;
                request.PageNumber = pageNumber;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                var response = await Client.POST<GetPOsResponse>(URI.POST_GetPOs, request);
                return response.PurchaseOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return list of purchase orders by declaring list of purchase order numbers
        /// </summary>
        /// <param name="poNumbers"></param>
        /// <param name="includeProducts"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async static Task<List<PurchaseOrder>> Get(List<string> poNumbers, bool includeProducts = true, int pageNumber = 0)
        {
            try
            {
                var token = API.Token;
                var request = new GetPOsRequest();
                request.PONumbers = poNumbers;
                request.IncludeProducts = includeProducts;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;
                var response = await Client.POST<GetPOsResponse>(URI.POST_GetPOs, request);
                return response.PurchaseOrders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<PurchaseOrder> Get(string poNumber, bool includeProducts = true, int pageNumber = 0)
        {
            try
            {
                var results = await Get(new List<string> { poNumber }, includeProducts, pageNumber);
                return results.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
