using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIComponents.ViewModels;
using WS.OrderHub.Managers;

namespace WS.OrderHub.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
            //MonitorJobs();
        }
        public static AppViewModel App { get => AppViewModel.Instance; }

        public ProgressBarViewModel MainProgressBar { get => ProgressBarViewModel.Instance; }
        public BannerViewModel MainBanner { get => BannerViewModel.Instance; }
        public UpdateOrdersViewModel UpdateOrders { get => UpdateOrdersViewModel.Instance; }


        private async void MonitorJobs()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    var job = JobManager.GetActiveAsync().Result;
                    if (job != null)
                    {
                        MainProgressBar.SetValue((int)job.Progress);
                        MainBanner.Show(job.Message, 0);
                    }
                }
            });
            
        }

    }
}
