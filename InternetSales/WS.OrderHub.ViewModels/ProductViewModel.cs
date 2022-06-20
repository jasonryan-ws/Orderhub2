using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        public readonly ProductModel model;
        public ProductViewModel(ProductModel model)
        {
            this.model = model;
        }
        public Guid Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }
        public string SKU
        {
            get => model.SKU;
            set => SetProperty(model.SKU, value, model, (m, p) => m.SKU = p);
        }
        public string UPC
        {
            get => model.UPC;
            set => SetProperty(model.UPC, value, model, (m, p) => m.UPC = p);
        }
        public string Name
        {
            get => model.Name;
            set => SetProperty(model.Name, value, model, (m, p) => m.Name = p);
        }
        public string ImageURL
        {
            get => model.ImageURL;
            set => SetProperty(model.ImageURL, value, model, (m, p) => m.ImageURL = p);
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
        public int Quantity
        {
            get => model.Quantity;
            set => SetProperty(model.Quantity, value, model, (m, p) => m.Quantity = p);
        }
        public decimal UnitPrice
        {
            get => model.UnitPrice;
            set => SetProperty(model.UnitPrice, value, model, (m, p) => m.UnitPrice = p);
        }
        public decimal Cost
        {
            get => model.Cost;
            set => SetProperty(model.Cost, value, model, (m, p) => m.Cost = p);
        }

        public string Image
        {
            get
            {
                var exists = false;
                try
                {
                    var request = (HttpWebRequest)HttpWebRequest.Create(ImageURL);
                    request.Method = "HEAD";
                    request.GetResponse();
                    exists = true;
                }
                catch { }
                return exists ?
                ImageURL : $@"{AppDomain.CurrentDomain.BaseDirectory}\assets\images\defaults\no_image.png";

            }
        }
    }
}
