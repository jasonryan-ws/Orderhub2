using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIComponents.ViewModels;

namespace UIComponents.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DialogControl.xaml
    /// </summary>
    public partial class DialogControl : UserControl
    {
        public DialogControl()
        {
            InitializeComponent();
            var vm = DataContext as DialogViewModel;
            if (vm == null)
                vm = DialogViewModel.Instance;
            vm.FocusInput += FocusInput;
        }

        private void FocusInput()
        { 
            InputText.Focus();
            InputText.SelectAll();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(ConfirmButton);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
            else if (e.Key == Key.Escape)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(CancelButton);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
        }
    }
}
