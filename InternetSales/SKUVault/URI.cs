using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault
{
    /// <summary>
    /// Collection of SKUVault API URIs
    /// </summary>
    public static class URI
    {
        private static readonly string BaseURI = "https://app.skuvault.com/api/";

        // BaseURI + Category + Method
        public static readonly string POST_GetTokens = BaseURI + "gettokens";

        // Purchase Order
        public static readonly string POST_GetPOs = BaseURI + "purchaseorders" + "/" + "getPOs";

        // Product
        public static readonly string POST_GetProducts = BaseURI + "products" + "/" + "getProducts";
        public static readonly string POST_GetProduct = BaseURI + "products" + "/" + "getProduct";
        public static readonly string POST_GetSuppliers = BaseURI + "products" + "/" + "getSuppliers";
        public static readonly string POST_GetClassifications = BaseURI + "products" + "/" + "getClassifications";

        // Inventory
        public static readonly string POST_GetWarehouses = BaseURI + "inventory" + "/" + "getWarehouses";
        public static readonly string POST_AddItem = BaseURI + "inventory" + "/" + "addItem";
        public static readonly string POST_RemoveItem = BaseURI + "inventory" + "/" + "removeItem";
        public static readonly string POST_SetItemQuantity = BaseURI + "inventory" + "/" + "setItemQuantity";
        
        public static readonly string POST_GetLocations = BaseURI + "inventory" + "/" + "getLocations";
        public static readonly string POST_GetItemQuantities = BaseURI + "inventory" + "/" + "getItemQuantities";
        public static readonly string POST_GetAvailableQuantities = BaseURI + "inventory" + "/" + "getAvailableQuantities";
        public static readonly string POST_GetInventoryByLocation = BaseURI + "inventory" + "/" + "getInventoryByLocation";
    }
}
