using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class ChannelViewModel : ObservableObject
    {
        public readonly ChannelModel model;
        public ChannelViewModel(ChannelModel model)
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
        public string Code
        {
            get => model.Code;
            set => SetProperty(model.Code, value, model, (m, p) => m.Code = p);
        }
        public int? ColorCode
        {
            get => model.ColorCode;
            set => SetProperty(model.ColorCode, value, model, (m, p) => m.ColorCode = p);
        }
        public Guid CreatedByNodeId
        {
            get => model.CreatedByNodeId;
            set => SetProperty(model.CreatedByNodeId, value, model, (m, p) => m.CreatedByNodeId = p);
        }
        public DateTime DateCreated
        {
            get => model.DateCreated;
            set => SetProperty(model.DateCreated, value, model, (m, p) => m.DateCreated = p);
        }
        public Guid? ModifiedByNodeId
        {
            get => model.ModifiedByNodeId;
            set => SetProperty(model.ModifiedByNodeId, value, model, (m, p) => m.ModifiedByNodeId = p);
        }
        public DateTime? DateModified
        {
            get => model.DateModified;
            set => SetProperty(model.DateModified, value, model, (m, p) => m.DateModified = p);
        }
        public bool IsDeleted
        {
            get => model.IsDeleted;
            set => SetProperty(model.IsDeleted, value, model, (m, p) => m.IsDeleted = p);
        }
        public DateTime? DateDeleted
        {
            get => model.DateDeleted;
            set => SetProperty(model.DateDeleted, value, model, (m, p) => m.DateDeleted = p);
        }
        public Guid? DeletedByNodeId
        {
            get => model.DeletedByNodeId;
            set => SetProperty(model.DeletedByNodeId, value, model, (m, p) => m.DeletedByNodeId = p);
        }
    }
}
