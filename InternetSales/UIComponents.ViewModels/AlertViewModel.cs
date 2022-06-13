using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Timers;
using System.Windows.Input;
using UIComponents.ViewModels.Modules;
using UIComponents.ViewModels.Visuals;
using Timer = System.Timers.Timer;

namespace UIComponents.ViewModels
{
    public class AlertViewModel : ObservableObject
    {
        Timer timer;
        int timeout;

        ResponseType responseType;
        public SubmitType SubmitType { get; set; }
        public AlertViewModel()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void StartTimer()
        {
            if (!timer.Enabled)
            {
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (--timeout == 0)
            {
                Close();
            }
        }

        #region MVVM Properties
        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (SetProperty(ref isOpen, value))
                {
                    GlobalViewModel.Instance.SetControlActivity();
                    Visibility = value ? "Visible" : "Collapsed";
                }
            }
        }

        private string visibility = "Collapsed";
        public string Visibility
        {
            get => visibility;
            set => SetProperty(ref visibility, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private string messageBarVisibility;
        public string MessageBarVisibility
        {
            get => messageBarVisibility;
            set => SetProperty(ref messageBarVisibility, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string titleBarVisibility;
        public string TitleBarVisibility
        {
            get => titleBarVisibility;
            set => SetProperty(ref titleBarVisibility, value);
        }

        private string iconName;
        public string IconName
        {
            get => iconName;
            set => SetProperty(ref iconName, value);
        }

        private string iconVisibility;
        public string IconVisibility
        {
            get => iconVisibility;
            set => SetProperty(ref iconVisibility, value);
        }

        private string confirmButtonText;
        public string ConfirmButtonText
        {
            get => confirmButtonText;
            set
            {
                SetProperty(ref confirmButtonText, value);
                ConfirmButtonVisibility = !string.IsNullOrWhiteSpace(value) ? "Visible" : "Collapsed";
            }
        }

        private bool confirmButtonIsEnabled;
        public bool ConfirmButtonIsEnabled
        {
            get => confirmButtonIsEnabled;
            set => SetProperty(ref confirmButtonIsEnabled, value);
        }

        private string confirmButtonVisibility;
        public string ConfirmButtonVisibility
        {
            get => confirmButtonVisibility;
            set => SetProperty(ref confirmButtonVisibility, value);
        }

        private string closeButtonText;
        public string CloseButtonText
        {
            get => closeButtonText;
            set
            {
                SetProperty(ref closeButtonText, value);
                CloseButtonVisibility = !string.IsNullOrWhiteSpace(value) ? "Visible" : "Collapsed";
            }
        }

        private string closeButtonVisibility;
        public string CloseButtonVisibility
        {
            get => closeButtonVisibility;
            set => SetProperty(ref closeButtonVisibility, value);
        }

        private bool closeButtonIsEnabled;
        public bool CloseButtonIsEnabled
        {
            get => closeButtonIsEnabled;
            set => SetProperty(ref closeButtonIsEnabled, value);
        }

        private string colorCode;
        public string ColorCode
        {
            get => colorCode;
            set => SetProperty(ref colorCode, value);
        }

        private string backColorCode;
        public string BackColorCode
        {
            get => backColorCode;
            set => SetProperty(ref backColorCode, value);
        }

        private string iconColorCode;
        public string IconColorCode
        {
            get => iconColorCode;
            set => SetProperty(ref iconColorCode, value);
        }

        private string messageColorCode;
        public string MessageColorCode
        {
            get => iconColorCode;
            set => SetProperty(ref messageColorCode, value);
        }

        private ProgressBarViewModel progressBarViewModel = new ProgressBarViewModel();
        public ProgressBarViewModel ProgressBarViewModel
        {
            get => progressBarViewModel;
            set => SetProperty(ref progressBarViewModel, value);
        }


        public ICommand ConfirmCommand => new RelayCommand(ExecuteConfirm);
        private void ExecuteConfirm()
        {
            responseType = ResponseType.Accepted;
            Close();
        }

        public ICommand CloseCommand => new RelayCommand(ExecuteClose);
        private void ExecuteClose()
        {
            Close();
        }

        #endregion

        public async Task<ResponseType> GetResponse()
        {
            await Task.Run(() =>
            {
                while (IsOpen) { }
            });
            return responseType;
        }


        public void Close()
        {
            IsOpen = false;
            timer.Stop();
        }

        // Timed close
        public void Close(int timeout)
        {
            if (timeout > 0)
            {
                this.timeout = timeout;
                StartTimer();
            }
        }

        // Initialize Alert
        public void Initialize(string message, string title, MessageType messageType, bool isTask, bool showIcon, int timeout)
        {
            Reset();
            CloseButtonText = "DISMISS";
            CloseButtonIsEnabled = true;
            Message = message;
            string alertTitle = InitializeMessageType(messageType);
            
            if (isTask)
            {
                alertTitle = "TASK";
                ColorCode = Colors.Task;
                IconName = "ProgressClock";
                CloseButtonText = null;
                timeout = 0;
            }
            Title = title != null ? title : alertTitle;
            IconVisibility = showIcon ? "Visible" : "Collapsed";
            IsOpen = true;
            Close(timeout);

        }

        public string InitializeMessageType(MessageType messageType)
        {
            string alertTitle;
            switch (messageType)
            {
                case MessageType.Warning:
                    alertTitle = "WARNING";
                    ColorCode = Colors.Warning;
                    BackColorCode = Colors.BackgroundWarning;
                    IconColorCode = Colors.Warning;
                    IconName = "AlertCircle";
                    break;
                case MessageType.Danger:
                    alertTitle = "ERROR";
                    ColorCode = Colors.Danger;
                    BackColorCode = Colors.BackgroundDanger;
                    IconColorCode = Colors.Danger;
                    IconName = "CloseCircle";
                    break;
                case MessageType.Success:
                    alertTitle = "SUCCESS";
                    ColorCode = Colors.Success;
                    BackColorCode = Colors.BackgroundSuccess;
                    IconColorCode = Colors.Success;
                    IconName = "CheckboxMarkedCircle";
                    break;
                default:
                    alertTitle = "INFO";
                    ColorCode = Colors.Info;
                    BackColorCode = Colors.BackgroundInfo;
                    IconColorCode = Colors.Info;
                    IconName = "Information";
                    break;
            }
            return alertTitle;
        }

        public void Initialize(string message, string title, SubmitType submitType, MessageType messageType, bool showIcon, string iconName = null)
        {
            Reset();
            Message = message;
            InitializeMessageType(messageType);
            if (title != null)
                Title = title;
            else
                Title = "CONFIRMATION";
            ConfirmButtonIsEnabled = true;
            CloseButtonIsEnabled = true;
            if (string.IsNullOrWhiteSpace(iconName))
                IconName = "QuestionMarkCircle";
            else
                IconName = iconName;
            SubmitType = submitType;
            switch (SubmitType)
            {
                case SubmitType.ConfirmCancel:
                    ConfirmButtonText = "CONFIRM";
                    CloseButtonText = "CANCEL";
                    break;
                case SubmitType.YesNo:
                    ConfirmButtonText = "YES";
                    CloseButtonText = "NO";
                    break;
                case SubmitType.AcceptReject:
                    ConfirmButtonText = "ACCEPT";
                    CloseButtonText = "REJECT";
                    break;
                default:
                    ConfirmButtonText = "SUBMIT";
                    CloseButtonText = "CANCEL";
                    break;
            }
            IconVisibility = showIcon ? "Visible" : "Collapsed";
            IsOpen = true;
        }

        private void Reset()
        {
            MessageBarVisibility = "Visible";
            TitleBarVisibility = "Visible";
            responseType = ResponseType.Rejected;
            ConfirmButtonText = null;
            CloseButtonText = null;
            ConfirmButtonIsEnabled = false;
            CloseButtonIsEnabled = false;
            ProgressBarViewModel.Collapse();
            ProgressBarViewModel.IsNotBusy = true;
        }


    }
}
