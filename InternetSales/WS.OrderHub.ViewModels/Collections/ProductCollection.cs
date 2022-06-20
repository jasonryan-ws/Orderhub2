using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels.Collections
{
    public class ProductCollection : ObservableCollection<ProductViewModel>
    {
        public ProductCollection(List<ProductViewModel> models)
        {
            foreach (var model in models)
            {
                Add(model);
            }
        }
        public ProductCollection(List<ProductModel> models)
        {
            if (models != null)
            {
                foreach (var model in models)
                {
                    var viewModel = new ProductViewModel(model);
                    Add(viewModel);
                }
            }
        }

        public ProductCollection()
        {
        }
    }
}
