using System.Threading.Tasks;
using UIComponents.ViewModels.Modules;

namespace UIComponents.ViewModels
{
    public static class Snackbar
    {
        public static bool? IsOpen { get => SnackbarViewModel.Instance.IsOpen; }
        public static void Close(int timeout = 0, bool enableControls = true)
        {
            if (timeout > 0)
                SnackbarViewModel.Instance.Close(timeout);
            else
                SnackbarViewModel.Instance.Close();

        }

        public static void Show(string message, int timeout = 5)
        {
            SnackbarViewModel.Instance.Initialize(message, null, MessageType.Info, false, false, timeout);
        }

        public static void Show(string message, MessageType messageType, int timeout = 5)
        {
            SnackbarViewModel.Instance.Initialize(message, null, messageType, false, false, timeout);
        }

        public static void Progress(string message)
        {
            SnackbarViewModel.Instance.Initialize(message, null, MessageType.Info, true, false, 0);
            SnackbarViewModel.Instance.ProgressBarViewModel.Show(false);
        }

        public static void Progress(string message, bool cancellable, bool enableControls)
        {
            SnackbarViewModel.Instance.Initialize(message, null, MessageType.Info, true, false, 0);
            if (cancellable)
            {
                SnackbarViewModel.Instance.CloseButtonText = "CANCEL";
                SnackbarViewModel.Instance.CloseButtonIsEnabled = true;
            }
            SnackbarViewModel.Instance.ProgressBarViewModel.Show(enableControls);
        }

        public static void Progress(string message, int count, int max, bool cancellable = false, bool enableControls = false)
        {
            
            if (cancellable)
            {
                SnackbarViewModel.Instance.CloseButtonText = "CANCEL";
                SnackbarViewModel.Instance.CloseButtonIsEnabled = true;
            }
            SnackbarViewModel.Instance.Initialize(message, null, MessageType.Info, true, false, 0);
            SnackbarViewModel.Instance.ProgressBarViewModel.SetValue(count, max, enableControls);
        }

        public static async Task<ResponseType> Confirm(string message, SubmitType confirmationType = SubmitType.AcceptReject, MessageType messageType = MessageType.Info)
        {
            SnackbarViewModel.Instance.Initialize(message, null, confirmationType, messageType, false);
            return await SnackbarViewModel.Instance.GetResponse();
        }
    }

    public class SnackbarViewModel : AlertViewModel
    {
        private static SnackbarViewModel instance;
        public static SnackbarViewModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new SnackbarViewModel();
                return instance;
            }
        }
    }
}