using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.ViewModels
{
    /// <summary>
    /// Holds the application information and configurations
    /// </summary>
    public class AppViewModel : ObservableObject
    {

        private static AppViewModel instance;
        public static AppViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppViewModel();
                return instance;
            }
        }
        public string Title { get => "OrderHub"; }


    }
}
