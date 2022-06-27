using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIComponents.ViewModels;
using UIComponents.ViewModels.Modules;
using WS.OrderHub.Managers;
using WS.OrderHub.ViewModels.Collections;
using WS.OrderHub.ViewModels.Objects;

namespace WS.OrderHub.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public static MainViewModel Instance { get; set; }
        public MainViewModel()
        {
            Instance = this;
            MonitorJobs();
        }
        public static AppViewModel App { get => AppViewModel.Instance; }

        public ProgressBarViewModel MainProgressBar { get => ProgressBarViewModel.Instance; }
        public DialogViewModel DialogViewModel { get => DialogViewModel.Instance; }
        public BannerViewModel MainBanner { get => BannerViewModel.Instance; }
        public UpdateOrdersViewModel UpdateOrders { get => UpdateOrdersViewModel.Instance; }

        private string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        private async void MonitorJobs()
        {
            await Task.Run(() =>
            {
                var anErrorHasOccured = false;
                while (true)
                {

                    try
                    {
                        var job = JobManager.GetActive();
                        var bannerWasOpenedByThis = false; // So it doesn't auto close the banner that was called outside from this method
                        if (job != null && job.StartedByNodeId != NodeManager.ActiveNode.Id)
                        {
                            MainProgressBar.SetValue(job.Progress);
                            if (MainBanner.IsOpen == null && job.Progress > 0)
                            {
                                MainBanner.Show(job.Message, 5);
                                bannerWasOpenedByThis = true;
                            }
                        }
                        else if (bannerWasOpenedByThis)
                            MainBanner.IsOpen = null;
                        else
                            MainProgressBar.Value = 100;
                        anErrorHasOccured = false;
                        bannerWasOpenedByThis = false;
                    }
                    catch (Exception ex)
                    {
                        if (!anErrorHasOccured)
                        {
                            // No need to reshow this error message again (until the reattemp is successful) after user dismisses the banner
                            MainBanner.Show(ex.Message, MessageType.Danger);
                            anErrorHasOccured = true;
                        }
                    }
                }
            });

        }

    }
}
