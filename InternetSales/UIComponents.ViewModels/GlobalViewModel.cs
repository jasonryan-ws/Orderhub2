using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIComponents.ViewModels
{
    public class GlobalViewModel : ObservableObject
    {
        private static GlobalViewModel instance;
        public static GlobalViewModel Instance
        {
            get
            { 
                if (instance == null)
                    instance = new GlobalViewModel();
                return instance;
            }
        }

        private bool isNotBusy = true;
        public bool IsNotBusy
        {
            get => isNotBusy;
            set => SetProperty(ref isNotBusy, value);
        }

        public void SetControlActivity(bool isInProgress = false)
        {
            IsNotBusy = !DialogViewModel.Instance.IsOpen && !ControlDialogViewModel.Instance.IsOpen && !isInProgress;
        }
    }
}
