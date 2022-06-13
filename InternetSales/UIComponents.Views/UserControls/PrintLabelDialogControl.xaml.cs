using System.Windows.Controls;
using UIComponents.ViewModels;

namespace UIComponents.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PrintLabelDialogControl.xaml
    /// </summary>
    public partial class PrintLabelDialogControl : UserControl
    {
        private readonly string fullRegistryKey;

        public PrintLabelDialogControl(string fullRegistryKey, string productCode, string productName, string additionalInfo = null, string title = null)
        {
            this.fullRegistryKey = fullRegistryKey;
            DataContext = new PrintLabelDialogViewModel(productCode, productName, additionalInfo, title);
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CopiesText.Focus();
            CopiesText.SelectAll();
            //try
            //{
            //    var size = RegistryManager.Read(fullRegistryKey, "Printers", "LabelSize", true);
            //    var type = RegistryManager.Read(fullRegistryKey, "Printers", "LabelType", true);
            //    var printer = RegistryManager.Read(fullRegistryKey, "Printers", "LabelPrinter", true);
            //    if (size != null)
            //        LabelSizeList.SelectedItem = size;
            //    else
            //        LabelSizeList.SelectedIndex = 0;

            //    if (type != null)
            //        LabelTypeList.SelectedItem = type;
            //    else
            //        LabelTypeList.SelectedIndex = 0;

            //    if (printer != null)
            //        PrinterList.SelectedItem = printer;
            //    else
            //    {
            //        if (PrinterList.Items.Count > 0)
            //            PrinterList.SelectedIndex = 0;
            //    }
            //}
            //catch
            //{
            //    LabelSizeList.SelectedIndex = 0;
            //    LabelTypeList.SelectedIndex = 0;
            //    if (PrinterList.Items.Count > 0)
            //        PrinterList.SelectedIndex = 0;
            //}
        }

        private void LabelSizeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = ((ComboBox)sender).SelectedItem;
            //RegistryManager.Write(fullRegistryKey, "Printers", "LabelSize", value);
        }

        private void LabelTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = ((ComboBox)sender).SelectedItem;
            //RegistryManager.Write(fullRegistryKey, "Printers", "LabelType", value);
        }

        private void PrinterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = ((ComboBox)sender).SelectedItem;
            //RegistryManager.Write(fullRegistryKey, "Printers", "LabelPrinter", value);
        }
    }
}
