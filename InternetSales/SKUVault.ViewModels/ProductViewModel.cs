using Microsoft.Toolkit.Mvvm.ComponentModel;
using SKUVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKUVault.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        public readonly Product model;
        public ProductViewModel(Product model)
        {
            this.model = model;
        }

        public string Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }
    }
}
