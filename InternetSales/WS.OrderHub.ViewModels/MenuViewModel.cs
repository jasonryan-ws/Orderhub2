using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.ViewModels.Objects;

namespace WS.OrderHub.ViewModels
{
    public class MenuViewModel : ObservableObject
    {

        public Action SwitchPage;

        private ObservableCollection<MenuGroup> menuGroups;
        public ObservableCollection<MenuGroup> MenuGroups
        {
            get => menuGroups;
            set => SetProperty(ref menuGroups, value);
        }
        public MenuViewModel()
        {
            // Load default menu groups
            MenuGroups = new ObservableCollection<MenuGroup>(DefaultData.MenuGroups.Load());
        }


    }
}
