using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIComponents.ViewModels;
using WS.OrderHub.ViewModels;
using WS.OrderHub.ViewModels.Objects;
using WS.OrderHub.Views.Pages;

namespace WS.OrderHub.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        ListBox previousListBox;
        public MenuControl()
        {
            InitializeComponent();
            var vm = DataContext as MenuViewModel;
            vm.SwitchPage += SwitchPage;
        }

        public void SwitchPage()
        {
            Banner.Show("Page switched");
        }

        private void MenuItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawerHost.CloseDrawerCommand.Execute(null, null);
            var listBox = (ListBox)sender;
            var item = (MenuGroupItem)listBox.SelectedItem;
            if (item != null)
            {
                dynamic page;
                switch (item.Name)
                {
                    case "Authorize Orders":
                        page = AuthorizeOrderPage.Instance;
                        break;
                    //case "Products":
                    //    page = ManageProductPage.Instance;
                    //    break;
                    //case "Purchase Orders":
                    //    page = PurchaseOrderPage.Instance;
                    //    break;
                    //case "Inventory":
                    //    page = InventoryPage.Instance;
                    //    break;
                    default:
                        page = InventoryPage.Instance;
                        break;
                }
                MainViewModel.Instance.PageTitle = item.Name;
                MainWindow.Instance.LoadPage(page);
            }
        }

        private void MenuItemListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (previousListBox != null)
                previousListBox.SelectedIndex = -1;

            var listBox = (ListBox)sender;
            listBox.SelectedItem = null;

        }

        private void MenuItemListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            previousListBox = (ListBox)sender;
        }
    }
}
