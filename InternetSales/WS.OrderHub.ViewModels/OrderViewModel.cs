using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIComponents.ViewModels;
using UIComponents.ViewModels.Visuals;
using WS.OrderHub.Managers;
using WS.OrderHub.Models;
using WS.OrderHub.ViewModels.Collections;

namespace WS.OrderHub.ViewModels
{

    public class OrderViewModel : ObservableObject
    {
        // Base Order Model
        public readonly OrderModel model;
        public OrderViewModel(OrderModel model)
        {
            this.model = model;
            if (model != null)
                SetTags();
            ShipAddressIsSelected = true;
        }

        private void SetTags()
        {
            var tags = new List<TagViewModel>();
            if (model.IsLocked)
                tags.Add(new TagViewModel { IconName = "Lock", IconColor = Colors.Alert, Name = "Locked", ToolTip = "Prevent other clients to load up this order by product code while it's being verified by this computer", IsDeletable = true, DeleteToolTip = "Unlock and unverify all items" });
            //tags.Add(new TagViewModel { IconName = "Store", IconColor = Colors.Alert, Name = Channel.Name });

            if (model.IsCancelled)
                tags.Add(new TagViewModel { IconName = "Close", IconColor = Colors.Danger, Name = "Cancelled" });
            if (model.IsVerified)
                tags.Add(new TagViewModel { IconName = "Check", IconColor = Colors.Info, Name = "Verified", IsDeletable = true, DeleteToolTip = "Unverify" });
            if (model.IsShipped)
                tags.Add(new TagViewModel { IconName = "Package", IconColor = Colors.Success, Name = "Shipped" });
            if (model.IsSkipped)
                tags.Add(new TagViewModel { IconName = "SkipForward", IconColor = Colors.Warning, Name = "Skipped", IsDeletable = true, DeleteToolTip = "Unskip" });

            Tags = tags;
        }


        private SearchBarViewModel searchBar = new SearchBarViewModel { Hint = "Scan Product Code or Order Number" };
        public SearchBarViewModel SearchBarViewModel
        {
            get => searchBar;
            set => SetProperty(ref searchBar, value);
        }

        public OrderViewModel()
        {
            this.model = new OrderModel();
            var order = OrderManager.Get("13223873", true, true);
            if (order != null)
            {
                this.model = order;
                ShipAddressIsSelected = true;
                SetTags();
            }
        }
        public Guid Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }

        public Guid ChannelId
        {
            get => model.ChannelId;
            set => SetProperty(model.ChannelId, value, model, (m, p) => m.ChannelId = p);
        }
        public DateTime DateOrdered
        {
            get => model.DateOrdered;
            set => SetProperty(model.DateOrdered, value, model, (m, p) => m.DateOrdered = p);
        }
        public string ChannelOrderNumber
        {
            get => model.ChannelOrderNumber;
            set => SetProperty(model.ChannelOrderNumber, value, model, (m, p) => m.ChannelOrderNumber = p);
        }
        public bool IsLocked
        {
            get => model.IsLocked;
            set => SetProperty(model.IsLocked, value, model, (m, p) => m.IsLocked = p);
        }
        public Guid? LockedByNodeId
        {
            get => model.LockedByNodeId;
            set => SetProperty(model.LockedByNodeId, value, model, (m, p) => m.LockedByNodeId = p);
        }
        public Guid BillAddressId
        {
            get => model.BillAddressId;
            set => SetProperty(model.BillAddressId, value, model, (m, p) => m.BillAddressId = p);
        }
        public Guid ShipAddressId
        {
            get => model.ShipAddressId;
            set => SetProperty(model.ShipAddressId, value, model, (m, p) => m.ShipAddressId = p);
        }
        public string Status
        {
            get => model.Status;
            set => SetProperty(model.Status, value, model, (m, p) => m.Status = p);
        }
        public bool IsVerified
        {
            get => model.IsVerified;
            set => SetProperty(model.IsVerified, value, model, (m, p) => m.IsVerified = p);
        }
        public DateTime? DateVerified
        {
            get => model.DateVerified;
            set => SetProperty(model.DateVerified, value, model, (m, p) => m.DateVerified = p);
        }
        public Guid? VerifiedByNodeId
        {
            get => model.VerifiedByNodeId;
            set => SetProperty(model.VerifiedByNodeId, value, model, (m, p) => m.VerifiedByNodeId = p);
        }
        public string ShipMethod
        {
            get => !string.IsNullOrWhiteSpace(model.ShipMethod) ? model.ShipMethod : "N/A";
            set => SetProperty(model.ShipMethod, value, model, (m, p) => m.ShipMethod = p);
        }
        public bool IsShipped
        {
            get => model.IsShipped;
            set => SetProperty(model.IsShipped, value, model, (m, p) => m.IsShipped = p);
        }
        public DateTime? DateShipped
        {
            get => model.DateShipped;
            set => SetProperty(model.DateShipped, value, model, (m, p) => m.DateShipped = p);
        }
        public decimal ShipCost
        {
            get => model.ShipCost;
            set => SetProperty(model.ShipCost, value, model, (m, p) => m.ShipCost = p);
        }
        public bool IsCancelled
        {
            get => model.IsCancelled;
            set => SetProperty(model.IsCancelled, value, model, (m, p) => m.IsCancelled = p);
        }
        public DateTime? DateCancelled
        {
            get => model.DateCancelled;
            set => SetProperty(model.DateCancelled, value, model, (m, p) => m.DateCancelled = p);
        }
        public Guid? CancelledByNodeId
        {
            get => model.CancelledByNodeId;
            set => SetProperty(model.CancelledByNodeId, value, model, (m, p) => m.CancelledByNodeId = p);
        }
        public bool IsSkipped
        {
            get => model.IsSkipped;
            set => SetProperty(model.IsSkipped, value, model, (m, p) => m.IsSkipped = p);
        }
        public DateTime? DateSkipped
        {
            get => model.DateSkipped;
            set => SetProperty(model.DateSkipped, value, model, (m, p) => m.DateSkipped = p);
        }
        public Guid? SkippedByNodeId
        {
            get => model.SkippedByNodeId;
            set => SetProperty(model.SkippedByNodeId, value, model, (m, p) => m.SkippedByNodeId = p);
        }
        public string Comments
        {
            get => model.Comments;
            set => SetProperty(model.Comments, value, model, (m, p) => m.Comments = p);
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
        public byte[] ExternalRowVersion
        {
            get => model.ExternalRowVersion;
            set => SetProperty(model.ExternalRowVersion, value, model, (m, p) => m.ExternalRowVersion = p);
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

        private IEnumerable<TagViewModel> tags;
        public IEnumerable<TagViewModel> Tags
        {
            get => tags;
            set => SetProperty(ref tags, value);
        }


        private bool shipAddressIsSelected;
        public bool ShipAddressIsSelected
        {
            get => shipAddressIsSelected;
            set
            {
                if (SetProperty(ref shipAddressIsSelected, value) && value)
                    Address = new AddressViewModel(model.ShipAddress);
            }
        }

        private bool billAddressIsSelected;
        public bool BillAddressIsSelected
        {
            get => billAddressIsSelected;
            set
            {
                if (SetProperty(ref billAddressIsSelected, value) && value)
                    Address = new AddressViewModel(model.BillAddress);
            }
        }


        private AddressViewModel address;
        public AddressViewModel Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }


        public ChannelViewModel Channel
        {
            get
            {
                return new ChannelViewModel(model.Channel);
            }
        }


        public ProductCollection Items
        {
            get => new ProductCollection(model.Items);
        }

        public ChargeCollection Charges
        {
            get => new ChargeCollection(model.Charges);
        }

        public int Units { get => Items != null ? Items.Sum(i => i.Quantity) : 0; }
        public decimal SubTotal { get => Items != null ? Items.Sum(i => i.Quantity * i.UnitPrice) : 0; }
        public decimal ChargeTotal { get => Charges != null ? Charges.Sum(c => c.Amount) : 0; }
        public decimal TotalAmount { get => SubTotal + ChargeTotal; }
        public ICommand DeleteCommand => new AsyncRelayCommand(DeleteAsync);
        private async Task DeleteAsync()
        {
            //var results = await OrderManager.DeleteAsync(Id);
        }


    }
}
