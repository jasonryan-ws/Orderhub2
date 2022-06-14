using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using UIComponents.ViewModels.Modules;

namespace UIComponents.ViewModels
{
    public static class Banner
    {
        public static void Dismiss(int timeout = 0)
        {
            if (timeout > 0)
                BannerViewModel.Instance.Close(timeout);
            else
                BannerViewModel.Instance.Close();
        }
        public static void Show(string message, int timeout = 5)
        {
            BannerViewModel.Instance.Initialize(message, null, MessageType.Info, false, true, timeout);
        }
        public static void Show(string message, bool showIcon)
        {
            BannerViewModel.Instance.Initialize(message, null, MessageType.Info, false, showIcon, 5);
        }

        public static void Show(string message, bool showIcon, int timeout = 5)
        {
            BannerViewModel.Instance.Initialize(message, null, MessageType.Info, false, showIcon, timeout);
        }


        public static void Show(string message, MessageType messageType, int timeout = 5)
        {
            BannerViewModel.Instance.Initialize(message, null, messageType, false, true, timeout);
        }

        public static void Show(string message, MessageType messageType, bool showIcon, int timeout = 5)
        {
            BannerViewModel.Instance.Initialize(message, null, messageType, false, showIcon, timeout);
        }


        public static async Task<ResponseType> Confirm(string message, SubmitType confirmationType = SubmitType.AcceptReject, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            BannerViewModel.Instance.Initialize(message, null, confirmationType, messageType, showIcon);
            return await BannerViewModel.Instance.GetResponse();
        }
    }

    public class BannerViewModel : AlertViewModel
    {
        private static BannerViewModel instance;
        public static BannerViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new BannerViewModel();
                return instance;
            }
        }

        private bool dismissed;
        public bool Dismissed
        {
            get => dismissed;
            set => SetProperty(ref dismissed, value);
        }

        public void Dismiss(int timeout = 0)
        {
            if (timeout > 0)
                Close(timeout);
            else
                Close();

        }
        public void Show(string message, int timeout = 5)
        {
            Initialize(message, null, MessageType.Info, false, true, timeout);
        }
        public void Show(string message, bool showIcon)
        {
            Initialize(message, null, MessageType.Info, false, showIcon, 5);
        }

        public void Show(string message, bool showIcon, int timeout = 5)
        {
            Initialize(message, null, MessageType.Info, false, showIcon, timeout);
        }

        public void Show(string message, string title, int timeout = 5)
        {
            Initialize(message, title, MessageType.Info, false, false, timeout);
        }

        public void Show(string message, string title, bool showIcon, int timeout = 5)
        {
            Initialize(message, title, MessageType.Info, false, showIcon, timeout);
        }

        public void Show(string message, MessageType messageType, int timeout = 5)
        {
            Initialize(message, null, messageType, false, true, timeout);
        }

        public void Show(string message, MessageType messageType, bool showIcon, int timeout = 5)
        {
            Initialize(message, null, messageType, false, showIcon, timeout);
        }

        public void Show(string message, string title, MessageType messageType, int timeout = 5)
        {
            Initialize(message, title, messageType, false, true, timeout);
        }

        public void Show(string message, string title, MessageType messageType, bool showIcon, int timeout = 5)
        {
           Initialize(message, title, messageType, false, showIcon, timeout);
        }

        public async Task<ResponseType> Confirm(string message, SubmitType confirmationType = SubmitType.AcceptReject, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            Initialize(message, null, confirmationType, messageType, showIcon);
            return await GetResponse();
        }

        public async Task<ResponseType> Confirm(string message, MessageType messageType, SubmitType confirmationType = SubmitType.AcceptReject, bool showIcon = true)
        {
            Initialize(message, null, confirmationType, messageType, showIcon);
            return await GetResponse();
        }
    }
}
