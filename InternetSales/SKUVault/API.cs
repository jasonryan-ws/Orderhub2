using Newtonsoft.Json;
using SKUVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SKUVault
{
    /// <summary>
    /// SKU API Documentation
    /// https://dev.skuvault.com/reference/
    /// </summary>
    public static class API
    {
        public static Token Token = new Token();
        public static int WarehouseId;
        

        public static void GetTokens()
        {
            Token = new Token
            {
                TenantToken = "l+a+ngqHMOm3/I0bUPNdBULMzdK7bmZhH6LTtXDzWts=",
                UserToken = "LgIbJs7fQxbRU6B/hylFN1+snMgD6DYhCDRPhxjnDGw=" 
            };
        }

        // Get tokens from the SKUVault server
        // SKUVault Email and Password are required
        // Returns true if successfull
        public async static Task<bool> GetTokens(Login login)
        {
            try
            {
                Token = await Client.POST<Token>("https://app.skuvault.com/api/gettokens", login);
                return Token != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verify SKUVault account and get API tokens
        /// Returns true if successfully verified
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async static Task<bool> VerifyAccount(string email, string password)
        {
            try
            {
                return await GetTokens(new Login { Email = email, Password = password });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
