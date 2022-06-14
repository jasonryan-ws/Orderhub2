using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIComponents.ViewModels;
using UIComponents.ViewModels.Modules;
using WS.OrderHub.Managers;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class UpdateOrdersViewModel : ObservableObject
    {

        private static UpdateOrdersViewModel instance;
        public static UpdateOrdersViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new UpdateOrdersViewModel();
                return instance;
            }
        }
        public UpdateOrdersViewModel()
        {
            ShipWorks.Configuration.Initialize(23005, "IS-SERVER", "SA", App.Decrypt("ZDiTJK7V0dY="), "ShipWorks");
        }

        public ProgressBarViewModel MainProgressBar { get => ProgressBarViewModel.Instance; }
        public BannerViewModel MainBanner { get => BannerViewModel.Instance; }

        public ICommand StartCommand => new AsyncRelayCommand(StartAsync);
        public async Task StartAsync()
        {
            try
            {
                MainProgressBar.Wait(enableControl: false);
                var maxRowVersion = OrderManager.LoadMaxRowVersion();
                var swOrders = await ShipWorks.OrderManager.GetAsync(true, maxRowVersion, 1);
                var job = new JobModel();
                job.MaxCount = swOrders.Count;
                JobManager.Start(job, TaskType.UpdateOrders);
                MainProgressBar.Hide();
                await Task.Run(() =>
                {
                    var count = 0;
                    foreach (var s in swOrders)
                    {
                        MainProgressBar.IsNotBusy = false;
                        JobManager.SetProgression(job.Id, ++count, $"Orders are being update by {job.StartedbyNode.Name}");
                        Dialog.Progress($"Updated {count}/{swOrders.Count}", "UPDATING ORDERS" , count, swOrders.Count);
                        //Thread.Sleep(100);
                    }
                    
                });
                job.Message = "Completed";
                Dialog.Show(job.Message, MessageType.Success);
                MainProgressBar.IsNotBusy = true;
                JobManager.End(job);
            }
            catch (Exception ex)
            {
                Dialog.Show(ex.Message, MessageType.Danger);
            }

        }
        //public async Task StartAsync()
        //{
        //    Dialog.Progress("Please wait...", "PREPARING");
        //    var count = 0;
        //    var maxRowVersion = OrderManager.LoadMaxRowVersion();
        //    var swOrders = await ShipWorks.OrderManager.GetAsync(true, maxRowVersion, 3); // Enter greater than zero if you're testing this app
        //    //OrderManager.ClearNewOrderStatus();
        //    foreach (var swo in swOrders)
        //    {
        //        Dialog.Progress($"{count}/{swOrders.Count}", $"PROCESSING", ++count, swOrders.Count);
        //        var order = new OrderModel();
        //        order.ChannelId = (await ChannelManager.GetAsync(swo.ChannelName)).Id;
        //        order.DateOrdered = swo.DateOrdered;
        //        order.ChannelOrderNumber = swo.OrderNumber;
        //        order.ShipMethod = swo.RequestedShipping;
        //        order.IsShipped = swo.DateShipped != null;
        //        order.Status = string.IsNullOrWhiteSpace(swo.LocalStatus) ? "Cleared" : swo.LocalStatus;
        //        order.DateShipped = swo.DateShipped;
        //        order.ShipCost = swo.ShipCost;
        //        order.IsCancelled = swo.IsCancelled;
        //        if (swo.IsCancelled)
        //        {
        //            order.Status = "Cancelled";
        //            order.DateCancelled = DateTime.Now;
        //            //order.IsPicked = false;
        //            //order.DatePicked = null;
        //        }
        //        order.Comments = swo.Comments;
        //        order.ExternalRowVersion = swo.RowVersion;

        //        order.BillAddress = new AddressModel();
        //        order.BillAddress.FirstName = swo.BillAddress.FirstName;
        //        order.BillAddress.MiddleName = swo.BillAddress.MiddleName;
        //        order.BillAddress.LastName = swo.BillAddress.LastName;
        //        order.BillAddress.Company = swo.BillAddress.Company;
        //        order.BillAddress.Street1 = swo.BillAddress.Street1;
        //        order.BillAddress.Street2 = swo.BillAddress.Street2;
        //        order.BillAddress.Street3 = swo.BillAddress.Street3;
        //        order.BillAddress.City = swo.BillAddress.City;
        //        order.BillAddress.State = swo.BillAddress.StateCode;
        //        order.BillAddress.PostalCode = swo.BillAddress.PostalCode;
        //        order.BillAddress.CountryCode = swo.BillAddress.CountryCode;
        //        order.BillAddress.Phone = swo.BillAddress.Phone;
        //        order.BillAddress.Fax = swo.BillAddress.Fax;
        //        order.BillAddress.Email = swo.BillAddress.Email;

        //        order.ShipAddress = new AddressModel();
        //        order.ShipAddress.FirstName = swo.ShipAddress.FirstName;
        //        order.ShipAddress.MiddleName = swo.ShipAddress.MiddleName;
        //        order.ShipAddress.LastName = swo.ShipAddress.LastName;
        //        order.ShipAddress.Company = swo.ShipAddress.Company;
        //        order.ShipAddress.Street1 = swo.ShipAddress.Street1;
        //        order.ShipAddress.Street2 = swo.ShipAddress.Street2;
        //        order.ShipAddress.Street3 = swo.ShipAddress.Street3;
        //        order.ShipAddress.City = swo.ShipAddress.City;
        //        order.ShipAddress.State = swo.ShipAddress.StateCode;
        //        order.ShipAddress.PostalCode = swo.ShipAddress.PostalCode;
        //        order.ShipAddress.CountryCode = swo.ShipAddress.CountryCode;
        //        order.ShipAddress.Phone = swo.ShipAddress.Phone;
        //        order.ShipAddress.Fax = swo.ShipAddress.Fax;
        //        order.ShipAddress.Email = swo.ShipAddress.Email;

        //        //await OrderManager.CreateAsync(order, true);

        //        foreach (var c in swo.Charges)
        //        {
        //            var charge = new ChargeModel();
        //            charge.Name = c.Description;
        //            await ChargeManager.CreateAsync(charge, true);
        //            OrderChargeManager.Create(order.Id, charge.Id, c.Amount);
        //        }

        //        foreach (var i in swo.Items)
        //        {
        //            // Create products
        //            var product = new ProductModel();
        //            product.SKU = i.SKU;
        //            product.UPC = i.UPC;
        //            product.Name = i.Name;
        //            product.ImageURL = i.ImageURL;
        //            ProductManager.Create(product, true);
        //            ProductBinManager.DeleteByProductId(product.Id);
        //            // Create bins
        //            foreach (var l in i.Locations)
        //            {
        //                var bin = new BinModel();
        //                bin.Name = l.Key;
        //                BinManager.Create(bin, true);
        //                ProductBinManager.Create(product.Id, bin.Id, l.Value);
        //            }

        //            OrderItemManager.Create(order.Id, product.Id, i.Quantity, i.Price);
        //        }
        //    }
        //    Dialog.Progress("Please wait...", "LOADING");
        //    CancelDeletedOrders();
        //    if (!silent)
        //    {
        //        if (sOrders.Count > 0)
        //        {
        //            var newOrders = OrderManager.LoadByStatus("New");
        //            if (newOrders.Count > 0)
        //            {
        //                Dialog.Show($"Received {newOrders.Count} new order(s).", "UP TO DATE", MessageType.Success, 10);
        //            }
        //            else
        //                Dialog.Show("No new orders received.", "UP TO DATE", MessageType.Success, 5);
        //        }
        //        else
        //            Dialog.Show($"No new updates are currently available", 5);
        //    }
        //    else
        //        Dialog.Close();
        //}
    }
}
