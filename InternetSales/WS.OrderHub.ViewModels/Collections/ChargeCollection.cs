using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels.Collections
{
    public class ChargeCollection : ObservableCollection<ChargeViewModel>
    {
        /// <summary>
        /// Call this property if base models are needed
        /// </summary>
        public readonly List<ChargeModel> models;
        /// <summary>
        /// Call this property if base view models are needed
        /// </summary>
        public readonly List<ChargeViewModel> viewModels;
        public ChargeCollection(List<ChargeViewModel> models)
        {
            this.viewModels = models;
            this.models = new List<ChargeModel>();
            foreach (var model in models)
            {
                this.models.Add(model.model);
                Add(model);
            }
        }

        public ChargeCollection(List<ChargeModel> models)
        {
            this.models = models;
            this.viewModels = new List<ChargeViewModel>();
            foreach (var model in models)
            {
                var vm = new ChargeViewModel(model);
                this.viewModels.Add(vm);
                Add(vm);
            }
        }
    }
}
