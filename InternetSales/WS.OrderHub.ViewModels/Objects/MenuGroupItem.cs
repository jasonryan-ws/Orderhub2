using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.ViewModels.Objects
{
    public class MenuGroupItem : ObservableObject
    {
        private int index;
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private string iconName;
        public string IconName
        {
            get => iconName;
            set => SetProperty(ref iconName, value);
        }
        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }
    }
}
