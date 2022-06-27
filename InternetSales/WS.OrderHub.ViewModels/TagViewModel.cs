using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.ViewModels
{
    public class TagViewModel : ObservableObject
    {
        private string iconName;
        public string IconName
        {
            get => iconName;
            set => SetProperty(ref iconName, value);
        }

        private string iconColor;
        public string IconColor
        {
            get => iconColor;
            set => SetProperty(ref iconColor, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string toolTip;
        public string ToolTip
        {
            get => toolTip;
            set => SetProperty(ref toolTip, value);
        }

        private bool isDeletable;
        public bool IsDeletable
        {
            get => isDeletable;
            set => SetProperty(ref isDeletable, value);
        }

        private string deleteToolTip;
        public string DeleteToolTip
        {
            get => deleteToolTip;
            set => SetProperty(ref deleteToolTip, value);
        }
    }
}
