using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class ChargeViewModel : ObservableObject
    {
        public readonly ChargeModel model;
        public ChargeViewModel(ChargeModel model)
        {
            this.model = model;
        }

        public Guid Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }
        public string Name
        {
            get => model.Name;
            set => SetProperty(model.Name, value, model, (m, p) => m.Name = p);
        }
        public string Description
        {
            get => model.Description;
            set => SetProperty(model.Description, value, model, (m, p) => m.Description = p);
        }
        public DateTime DateCreated
        {
            get => model.DateCreated;
            set => SetProperty(model.DateCreated, value, model, (m, p) => m.DateCreated = p);
        }
        public Guid CreatedByNodeId
        {
            get => model.CreatedByNodeId;
            set => SetProperty(model.CreatedByNodeId, value, model, (m, p) => m.CreatedByNodeId = p);
        }
        public DateTime? DateModified
        {
            get => model.DateModified;
            set => SetProperty(model.DateModified, value, model, (m, p) => m.DateModified = p);
        }
        public Guid? ModifiedByNodeId
        {
            get => model.ModifiedByNodeId;
            set => SetProperty(model.ModifiedByNodeId, value, model, (m, p) => m.ModifiedByNodeId = p);
        }
        public decimal Amount
        {
            get => model.Amount;
            set => SetProperty(model.Amount, value, model, (m, p) => m.Amount = p);
        }


    }
}
