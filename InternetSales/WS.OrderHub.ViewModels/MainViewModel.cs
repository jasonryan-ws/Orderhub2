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

namespace WS.OrderHub.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            MonitorJobs();
        }
        public static AppViewModel App { get => AppViewModel.Instance; }

        public ProgressBarViewModel MainProgressBar { get => ProgressBarViewModel.Instance; }
        public DialogViewModel DialogViewModel { get => DialogViewModel.Instance; }
        public BannerViewModel MainBanner { get => BannerViewModel.Instance; }
        public UpdateOrdersViewModel UpdateOrders { get => UpdateOrdersViewModel.Instance; }


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
                        if (job != null && job.StartedByNodeId != NodeManager.ActiveNode.Id)
                        {
                            MainProgressBar.SetValue(job.Progress);
                            if (MainBanner.IsOpen == null && job.Progress > 0)
                                MainBanner.Show(job.Message, 5);
                        }
                        else
                        {
                            MainBanner.IsOpen = null;
                            MainProgressBar.Value = 100;
                        }
                        anErrorHasOccured = false;
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
