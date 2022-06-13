using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using UIComponents.ViewModels.Modules;

namespace UIComponents.ViewModels
{
    public static class Dialog
    {
        public static bool IsOpen { get => DialogViewModel.Instance.IsOpen; }
        public static SubmitType InputType { get; set; }
        public static void Close(int timeout = 0)
        {
            if (timeout > 0)
                DialogViewModel.Instance.Close(timeout);
            else
                DialogViewModel.Instance.Close();
        }
        public static void Show(string message, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, MessageType.Info, false, true, timeout);
        }

        public static void Show(string message, bool showIcon, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, MessageType.Info, false, showIcon, timeout);
        }

        public static void Show(string message, string title, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, MessageType.Info, false, true, timeout);
        }

        public static void Show(string message, string title, bool showicon, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, MessageType.Info, false, showicon, timeout);
        }

        public static void Show(string message, MessageType messageType, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, messageType, false, true, timeout);
        }

        public static void Show(string message, MessageType messageType, bool showIcon, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, messageType, false, showIcon, timeout);
        }

        public static void Show(string message, string title, MessageType messageType, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, messageType, false, true, timeout);
        }

        public static void Show(string message, string title, MessageType messageType, bool showIcon, int timeout = 0)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, messageType, false, showIcon, timeout);
        }


        public static void Progress(string message, bool cancellable = false)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, MessageType.Info, true, false, 0);
            if (cancellable)
            {
                DialogViewModel.Instance.CloseButtonText = "CANCEL";
                DialogViewModel.Instance.CloseButtonIsEnabled = true;
            }
            DialogViewModel.Instance.ProgressBarViewModel.Show();
        }

        public static void Progress(string message, string title, bool cancellable = false)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, MessageType.Info, true, false, 0);
            if (cancellable)
            {
                DialogViewModel.Instance.CloseButtonText = "CANCEL";
                DialogViewModel.Instance.CloseButtonIsEnabled = true;
            }
            DialogViewModel.Instance.ProgressBarViewModel.Show();
        }

        public static void Progress(string message, int count, int max, bool cancellable = false)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, MessageType.Info, true, false, 0);
            DialogViewModel.Instance.ProgressBarViewModel.SetValue(count, max);
            if (cancellable)
            {
                DialogViewModel.Instance.CloseButtonText = "CANCEL";
                DialogViewModel.Instance.CloseButtonIsEnabled = true;
            }
        }
        public static void Progress(string message, string title, int count, int max, bool cancellable = false)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, title, MessageType.Info, true, false, 0);
            DialogViewModel.Instance.ProgressBarViewModel.SetValue(count, max);
            if (cancellable)
            {
                DialogViewModel.Instance.CloseButtonText = "CANCEL";
                DialogViewModel.Instance.CloseButtonIsEnabled = true;
            }
        }

        public static async Task<ResponseType> Confirm(string message, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, SubmitType.AcceptReject, messageType, showIcon);
            return await DialogViewModel.Instance.GetResponse();
        }

        public static async Task<ResponseType> Confirm(string message, SubmitType confirmationType = SubmitType.AcceptReject, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.Initialize(message, null, confirmationType, messageType, showIcon);
            return await DialogViewModel.Instance.GetResponse();
        }


        public static async Task<ResponseType> Confirm(string message, string confirmationCode, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.ConfirmationCode = confirmationCode;
            DialogViewModel.Instance.Hint = $"Confirmation Code: {confirmationCode}";
            DialogViewModel.Instance.InputTextVisibility = "Visible";
            DialogViewModel.Instance.Initialize(message, null, SubmitType.ConfirmCancel, messageType, showIcon);
            DialogViewModel.Instance.ConfirmButtonIsEnabled = false;

            return await DialogViewModel.Instance.GetResponse();
        }

        public static async Task<ResponseType> Confirm(string message, string title, string confirmationCode, MessageType messageType = MessageType.Info, bool showIcon = true)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.ConfirmationCode = confirmationCode;
            DialogViewModel.Instance.Hint = $"Confirmation Code: {confirmationCode}";
            DialogViewModel.Instance.InputTextVisibility = "Visible";
            DialogViewModel.Instance.Initialize(message, title, SubmitType.ConfirmCancel, messageType, showIcon);
            DialogViewModel.Instance.ConfirmButtonIsEnabled = false;

            return await DialogViewModel.Instance.GetResponse();
        }

        //public static async Task<ResponseType> Input(string message, string hint = "Value", MessageType messageType = MessageType.Info, bool showIcon = true)
        //{
        //    DialogViewModel.Instance.Reset();
        //    DialogViewModel.Instance.ConfirmationCode = confirmationCode;
        //    DialogViewModel.Instance.Hint = $"Confirmation Code: {confirmationCode}";
        //    DialogViewModel.Instance.InputTextVisibility = "Visible";
        //    DialogViewModel.Instance.Initialize(message, null, SubmitType.ConfirmCancel, messageType, showIcon);
        //    DialogViewModel.Instance.ConfirmButtonIsEnabled = false;

        //    return await DialogViewModel.Instance.GetResponse();
        //}

        //public static async Task<ResponseType> Input(string message, string text, string hint, MessageType messageType = MessageType.Info, bool showIcon = true)
        //{
        //    DialogViewModel.Instance.Reset();
        //    DialogViewModel.Instance.Initialize(message, null, SubmitType.Text, messageType, showIcon);
        //    return await DialogViewModel.Instance.GetResponse();
        //}

        public static async Task<object> Input(string title, object value = null, string iconName = null, string hint = null)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.CurrentValue = value == null ? string.Empty : value.ToString();
            DialogViewModel.Instance.InputText = DialogViewModel.Instance.CurrentValue;
            DialogViewModel.Instance.Hint = hint;
            if (title == null)
                title = string.Empty;
            DialogViewModel.Instance.Initialize(null, title, SubmitType.Input, MessageType.Info, !string.IsNullOrWhiteSpace(iconName), iconName);
            DialogViewModel.Instance.ConfirmButtonIsEnabled = false;
            DialogViewModel.Instance.InputTextVisibility = "Visible";
            DialogViewModel.Instance.MessageBarVisibility = "Collapsed";
            DialogViewModel.Instance.EmptyTextIsAllowed = true;
            DialogViewModel.Instance.TitleBarVisibility = title != null ? "Visible" : "Collapsed";
            DialogViewModel.Instance.FocusInput();
            var response = await DialogViewModel.Instance.GetResponse();
            if (response == ResponseType.Accepted)
                return DialogViewModel.Instance.InputText;
            else
                return null;
        }

        public static async Task<object> Input(string title, object value, bool emptyTextIsAllowed = false, string iconName = null, string hint = null)
        {
            DialogViewModel.Instance.Reset();
            DialogViewModel.Instance.CurrentValue = value == null ? string.Empty : value.ToString();
            DialogViewModel.Instance.InputText = DialogViewModel.Instance.CurrentValue; 
            DialogViewModel.Instance.Hint = hint;
            
            if (title == null)
                title = string.Empty;
            DialogViewModel.Instance.Initialize(null, title, SubmitType.Input, MessageType.Info, !string.IsNullOrWhiteSpace(iconName), iconName);
            DialogViewModel.Instance.ConfirmButtonIsEnabled = false;
            DialogViewModel.Instance.InputTextVisibility = "Visible";
            DialogViewModel.Instance.MessageBarVisibility = "Collapsed";
            DialogViewModel.Instance.TitleBarVisibility = title != null ? "Visible" : "Collapsed";
            DialogViewModel.Instance.EmptyTextIsAllowed = emptyTextIsAllowed;
            DialogViewModel.Instance.FocusInput();
            var response = await DialogViewModel.Instance.GetResponse();
            if (response == ResponseType.Accepted)
                return DialogViewModel.Instance.InputText;
            else
                return null;
        }
    }

    public class DialogViewModel : AlertViewModel
    {
        public Action FocusInput { get; set; }
        public bool EmptyTextIsAllowed { get; set; }
        public string CurrentValue { get; set; }
        public string ConfirmationCode { get; set; }
        private static DialogViewModel instance;
        public static DialogViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new DialogViewModel();
                return instance;
            }
        }

        private string inputText = string.Empty;
        public string InputText
        {
            get => inputText;
            set
            {
                if (SetProperty(ref inputText, value))
                {
                    if (SubmitType == SubmitType.ConfirmCancel)
                    {
                        if (!string.IsNullOrEmpty(ConfirmationCode))
                            ConfirmButtonIsEnabled = ConfirmationCode.ToUpper() == value.ToUpper();
                    }
                    else if (EmptyTextIsAllowed)
                        ConfirmButtonIsEnabled = true && CurrentValue != value;
                    else
                        ConfirmButtonIsEnabled = !string.IsNullOrWhiteSpace(value) && CurrentValue != value;
                }
            }

        }

        private string inputTextVisibility;
        public string InputTextVisibility
        {
            get => inputTextVisibility;
            set => SetProperty(ref inputTextVisibility, value);
        }

        private string inputTextAlignment;
        public string InputTextAlignment
        {
            get => inputTextAlignment;
            set => SetProperty(ref inputTextAlignment, value);
        }

        private string hint;
        public string Hint
        {
            get => hint;
            set
            {
                SetProperty(ref hint, value);
                InputTextAlignment = value != null ? "Left" : "Center";
            }
        }


        public void Reset()
        {
            ConfirmationCode = null;
            InputTextVisibility = "Collapsed";
            InputText = null;
        }

        //public ICommand CloseCommand => new RelayCommand(Close)
    }
}
