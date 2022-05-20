using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.ViewModels.Objects;

namespace WS.OrderHub.ViewModels.DefaultData
{
    public static class MenuGroups
    {

        public static List<MenuGroup> Load()
        {
            var groups = new List<MenuGroup>();
            groups.Add(LoadMainMenuItems());
            groups.Add(LoadSKUVaultMenuItems());
            return groups;
        }

        public static MenuGroup LoadMainMenuItems()
        {
            var group = new MenuGroup();
            group.Index = 0;
            group.Name = "MAIN MENU";
            group.Items = new List<MenuGroupItem>();

            group.Items.Add(new MenuGroupItem
            {
                Index = (group.Items.Count + 1) + group.Index,
                Name = "Authorize Orders",
                Description = "Verify and print order tags and receipts",
                IconName = "BarcodeScanner"
            });

            return group;
        }

        public static MenuGroup LoadSKUVaultMenuItems()
        {
            var group = new MenuGroup();
            group.Index = 0;
            group.Name = "SKUVault";
            group.Items = new List<MenuGroupItem>();

            group.Items.Add(new MenuGroupItem
            {
                Index = (group.Items.Count + 1) + group.Index,
                Name = "Inventory",
                Description = "Manage inventory and print product labels",
                IconName = "Warehouse"
            });

            return group;
        }
    }
}
