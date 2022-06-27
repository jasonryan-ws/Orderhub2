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

namespace WS.OrderHub.Views.Pages
{
    /// <summary>
    /// Interaction logic for InventoryPage.xaml
    /// </summary>
    /// 
    public partial class InventoryPage : Page
    {
        private static InventoryPage instance;
        public static InventoryPage Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryPage();
                return instance;
            }
        }
        public InventoryPage()
        {
            InitializeComponent();
        }
    }
}
