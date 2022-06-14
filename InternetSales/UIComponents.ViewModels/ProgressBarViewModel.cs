using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using UIComponents.ViewModels.Modules;
using UIComponents.ViewModels.Visuals;

namespace UIComponents.ViewModels
{

    public static class PageProgressBar
    {
        private static ProgressBarViewModel instance;
        public static ProgressBarViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProgressBarViewModel();
                return instance;
            }
        }

        public static void SetValue(int value, bool enableControl = true)
        {
            Instance.SetValue(value, enableControl);
        }

        public static void SetValue(int value, string text, bool enableControl = true)
        {
            SetValue(value, enableControl);
            Instance.Text = text;
        }

        public static void Show(bool enableControl = true)
        {
            Instance.Show(enableControl);
        }

        public static void Show(int value, bool enableControl = true)
        {
            Instance.Show(value, enableControl);
        }

        public static void Complete(string text, string progress = "100%")
        {
            Instance.Complete(text, progress);
        }

        public static void Wait(string text = null, string progress = "Please wait...", bool enableControl = false)
        {
            Instance.Wait(text, progress, enableControl);
        }
        public static void Hide()
        {
            Instance.Hide();
        }

        public static void Collapse()
        {
            Instance.Collapse();
        }

        public static int? SetValue(int count, int max, bool enableControl = true)
        {
            return Instance.SetValue(count, max, enableControl);
        }
    }

    public class ProgressBarViewModel : ObservableObject
    {
        private static ProgressBarViewModel instance;
        public static ProgressBarViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProgressBarViewModel();
                return instance;
            }
        }


        private int? value;
        public int? Value
        {
            get => value;
            set
            {
                if (SetProperty(ref this.value, value))
                {
                    if (value != null)
                    {
                        Visibility = "Visible";
                        IsIndeterminate = false;
                        Progress = $"{value}%";
                    }
                    else
                    {
                        Visibility = "Collapsed";
                        IsIndeterminate = true;
                        Progress = null;
                    }
                }
            }
        }


        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private string progress;
        public string Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private bool isIndeterminate;
        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set => SetProperty(ref isIndeterminate, value);
        }

        private bool isNotBusy = true;
        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    GlobalViewModel.Instance.SetControlActivity(!IsNotBusy);
            }
        }

        private string visibility = "Hidden";
        public string Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        private string backColor;
        public string BackColor
        {
            get => backColor;
            set => SetProperty(ref backColor, value);
        }

        private string foreground = Colors.FontDark;
        public string Foreground
        {
            get => foreground;
            set => SetProperty(ref foreground, value);
        }

        public void Show(bool enableControl = true)
        {
            IsNotBusy = enableControl;
            IsIndeterminate = true;
            Progress = null;
            Value = null;
            Visibility = "Visible";
        }

        public int Show(int value, bool enableControl = true)
        {
            Value = value;
            IsNotBusy = enableControl;
            IsIndeterminate = false;
            Visibility = "Visible";
            return value;
        }

        public void Hide(bool isNotBusy = true)
        {
            Value = null;
            IsNotBusy = isNotBusy;
            IsIndeterminate = false;
            Visibility = "Hidden";
        }

        public void Collapse()
        {
            Value = null;
            IsNotBusy = true;
            IsIndeterminate = false;
            Visibility = "Collapsed";
        }

        public void Complete(string text, string progress = "100%", MessageType messageType = MessageType.Default, int value = 100)
        {
            if (messageType == MessageType.Info)
                Foreground = Colors.Info;
            else if (messageType == MessageType.Warning)
                Foreground = Colors.Warning;
            else if (messageType == MessageType.Danger)
                Foreground = Colors.Danger;
            else if (messageType == MessageType.Success)
                Foreground = Colors.Success;
            else
                Foreground = Colors.FontDark;
            Visibility = "Visible";
            Value = value;
            Text = text;
            IsIndeterminate = false;
            Progress = progress;
            IsNotBusy = true;
        }

        public void Wait(string text = null, string progress = "Please wait...", bool enableControl = false)
        {
            BackColor = null;
            Visibility = "Visible";
            Text = text;
            Progress = progress;
            IsNotBusy = enableControl;
            IsIndeterminate = true;
        }
        public void SetValue(int value, bool enableControl = true)
        {
            BackColor = null;
            Value = value;
            IsNotBusy = enableControl;
        }

        public void SetValue(int value, string text, bool enableControl = true)
        {
            SetValue(value, enableControl);
            Text = text;
        }

        public int? SetValue(int count, int max, bool enableControl = true)
        {
            Value = count * 100 / max;
            IsNotBusy = enableControl;
            return Value;
        }
    }
}
