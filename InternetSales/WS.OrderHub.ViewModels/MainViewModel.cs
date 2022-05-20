using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public static AppViewModel App { get => AppViewModel.Instance; }
    }
}
