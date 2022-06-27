using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.OrderHub.ViewModels.Objects
{
    public class MenuGroup : ObservableObject
    {
        private int index;
        public int Index
        {
            get => index;
            set => SetProperty(ref index, value);
        }
        /// <summary>
        /// Menu title - Expander title
        /// </summary>
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        /// <summary>
        /// Menu selections
        /// </summary>
        public List<MenuGroupItem> Items { get; set; }

    }
}
