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
    public static class InventoryManager
    {
        public async static Task<Response> AddItemQuantity(string sku, int warehouseId, string locationCode, int quantity, TransactionType reason = TransactionType.Add, string note = null, List<string> serialNumbers = null)
        {
            try
            {
                var token = API.Token;
                var request = new InventoryRequest();

                request.Sku = sku;
                request.WarehouseId = warehouseId;
                request.LocationCode = locationCode;
                request.Quantity = quantity;
                request.Reason = reason.ToString();
                request.Note = note;
                request.SerialNumbers = serialNumbers;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;

                var response = await Client.POST<AddItemResponse>(URI.POST_AddItem, request);
                return response.Status;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async static Task<Response> RemoveItemQuantity(string sku, int warehouseId, string locationCode, int quantity, TransactionType reason = TransactionType.Remove, List<string> serialNumbers = null)
        {
            try
            {
                var token = API.Token;
                var request = new InventoryRequest();

                request.Sku = sku;
                request.WarehouseId = warehouseId;
                request.LocationCode = locationCode;
                request.Quantity = quantity;
                request.Reason = reason.ToString();
                request.SerialNumbers = serialNumbers;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;

                var response = await Client.POST<RemoveItemResponse>(URI.POST_RemoveItem, request);
                return (Response)Enum.Parse(typeof(Response), response.RemoveItemStatus);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async static Task<Response> SetItemQuantity(string sku, int warehouseId, string locationCode, int quantity)
        {
            try
            {
                var token = API.Token;
                var request = new InventoryRequest();

                request.Sku = sku;
                request.WarehouseId = warehouseId;
                request.LocationCode = locationCode;
                request.Quantity = quantity;
                request.TenantToken = token.TenantToken;
                request.UserToken = token.UserToken;

                var response = await Client.POST<SetItemQuantityResponse>(URI.POST_SetItemQuantity, request);
                return response.Result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
