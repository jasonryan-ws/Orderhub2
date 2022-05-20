using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WS.OrderHub.Managers;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        // Base Order Model
        public readonly OrderModel model;
        public OrderViewModel(OrderModel model)
        {
            this.model = model;
        }

        public Guid Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }

        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteAsync);
        private async Task DeleteAsync()
        {
            var results = await OrderManager.DeleteAsync(Id);
        }

    }
}
