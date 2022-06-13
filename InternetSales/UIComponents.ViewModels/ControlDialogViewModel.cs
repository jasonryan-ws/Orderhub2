using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace UIComponents.ViewModels
{

    public static class ControlDialog
    {
        public static void Close()
        {
            ControlDialogViewModel.Instance.Close();
        }
    }

    public class ControlDialogViewModel : ObservableObject
    {
        private bool isNotBusy = true;
        public bool IsNotBusy
        {
            get => isNotBusy;
            set => SetProperty(ref isNotBusy, value);
        }
        public void Close()
        {
            IsNotBusy = true;
            IsOpen = false;
        }

        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (SetProperty(ref isOpen, value))
                {
                    GlobalViewModel.Instance.SetControlActivity();
                    IsNotBusy = !value;
                }
            }
        }

        private static ControlDialogViewModel instance;
        public static ControlDialogViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ControlDialogViewModel();
                return instance;
            }
        }
    }
}
