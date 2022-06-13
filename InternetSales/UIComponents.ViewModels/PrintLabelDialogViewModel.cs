using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Drawing.Printing;
using System.Timers;
using System.Windows.Input;
using Utilities.Reporting.Components;
using Utilities.Reporting.Models;
using Timer = System.Timers.Timer;

namespace UIComponents.ViewModels
{
    public class PrintLabelDialogViewModel : ObservableObject
    {
        Timer timer;
        int delay;

        public readonly ProductLabelReport model;
        public ProductLabelReport defaultData;

        public PrintLabelDialogViewModel()
        {
            InitializeTimer();
        }
        public PrintLabelDialogViewModel(ProductLabelReport model)
        {
            InitializeTimer();
            this.model = model;
            
        }
        public PrintLabelDialogViewModel(string productCode, string productName, string additionalInfo = "", string title = null)
        {
            InitializeTimer();
            model = new ProductLabelReport();
            ProductCode = productCode;
            ProductName = productName;
            AdditionalInfo = additionalInfo;
            defaultData = new ProductLabelReport();
            defaultData.ProductCode = productCode;
            defaultData.ProductName = productName;
            defaultData.AdditionalInfo = additionalInfo;
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            //timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void StartPreviewTimer()
        {
            ProgressBar.Show();
            PreviewImage = null;
            delay = 2;
            if (!timer.Enabled)
                timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (--delay == 0)
            {
                timer.Stop();
                await GeneratePreviewImage();
                ProgressBar.Hide();
            }
        }

        private string title = "Print Label";
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string ProductCode
        {
            get => model.ProductCode;
            set
            {
                if (SetProperty(model.ProductCode, value, model, (m, p) => m.ProductCode = p))
                    StartPreviewTimer();
            }
        }

        public string ProductName
        {
            get => model.ProductName;
            set
            {
                if (SetProperty(model.ProductName, value, model, (m, p) => m.ProductName = p))
                    StartPreviewTimer();
            }
        }

        public string AdditionalInfo
        {
            get => model.AdditionalInfo;
            set
            {
                if (SetProperty(model.AdditionalInfo, value, model, (m, p) => m.AdditionalInfo = p))
                    StartPreviewTimer();
            }
        }

        private string labelSize;
        public string LabelSize
        {
            get => labelSize;
            set
            {
                if (SetProperty(ref labelSize, value))
                    StartPreviewTimer();
            }
        }

        private string labelType;
        public string LabelType
        {
            get => labelType;
            set
            {
                if (SetProperty(ref labelType, value))
                {
                    AdditionlInfoIsNotBusy = Utilities.Generic.ParseEnum<LabelType>(value) != Utilities.Reporting.Components.LabelType.Box;
                    StartPreviewTimer();
                }
            }
        }

        private bool additionlInfoIsNotBusy;
        public bool AdditionlInfoIsNotBusy
        {
            get => additionlInfoIsNotBusy;
            set => SetProperty(ref additionlInfoIsNotBusy, value);
        }


        //public LabelType LabelType
        //{
        //    get => model.LabelType;
        //    set
        //    {
        //        if (SetProperty(model.LabelType, value, model, (m, p) => m.LabelType = p))
        //            StartPreviewTimer();
        //    }
        //}

        public Alignment Alignment
        {
            get => model.Alignment;
            set
            {
                if (SetProperty(model.Alignment, value, model, (m, p) => m.Alignment = p))
                    StartPreviewTimer();
            }
        }

        private byte[] previewImage;
        public byte[] PreviewImage
        {
            get => previewImage;
            set => SetProperty(ref previewImage, value);
        }

        public string PrinterName
        {
            get => model.PrinterName;
            set => SetProperty(model.PrinterName, value, model, (m, p) => m.PrinterName = p);
        }

        private int copies = 1;
        public int Copies
        {
            get => copies;
            set
            {
                if (value > 0)
                    SetProperty(ref copies, value);
            }
        }

        public ICommand ResetCommand => new RelayCommand(ExecuteReset);
        private void ExecuteReset()
        {
            ProductCode = defaultData.ProductCode;
            ProductName = defaultData.ProductName;
            AdditionalInfo = defaultData.AdditionalInfo;
        }

        public ICommand CloseCommand => new RelayCommand(ExecuteClose);
        private void ExecuteClose()
        {
            ControlDialog.Close();
        }

        public ICommand NumUpCommand => new RelayCommand(ExecuteNumUp);
        private void ExecuteNumUp()
        {
            Copies++;
        }

        public ICommand NumDownCommand => new RelayCommand(ExecuteNumDown);
        private void ExecuteNumDown()
        {
            Copies--;
        }

        public ICommand PrintCommand => new RelayCommand(ExecutePrint);
        private async void ExecutePrint()
        {
            ControlDialog.Close();
            Snackbar.Show("Printing...");
            await Task.Run(() =>
            {

                Alignment = Alignment.Center;
                model.LabelType = Utilities.Generic.ParseEnum<LabelType>(LabelType);
                model.LabelSize = Utilities.Generic.GetEnumValueFromDescription<Size>(LabelSize);
                model.Print(Copies);
                Snackbar.Close();
            });

        }

        public ICommand RefreshCommand => new RelayCommand(ExecuteRefresh);
        private void ExecuteRefresh()
        {
            StartPreviewTimer();
        }

        private async Task GeneratePreviewImage()
        {
            await Task.Run(() =>
            {
                Alignment = Alignment.Center;
                model.LabelType = Utilities.Generic.ParseEnum<LabelType>(LabelType);
                model.LabelSize = Utilities.Generic.GetEnumValueFromDescription<Size>(LabelSize);
                PreviewImage = model.GetBytes();
            });
        }

        public IEnumerable<string> LabelSizes
        {
            get
            {
                return from Size n
                       in Enum.GetValues(typeof(Size))
                       select Utilities.Generic.GetEnumDescription(n);
            }
        }

        public IEnumerable<string> LabelTypes
        {
            get
            {
                return from LabelType n
                       in Enum.GetValues(typeof(LabelType))
                       select n.ToString();
            }

        }
        public IEnumerable<string> Printers
        {
            get
            {
                return from string printer
                       in PrinterSettings.InstalledPrinters
                       select printer;
            }
        }

        private ProgressBarViewModel progresBar = new ProgressBarViewModel();
        public ProgressBarViewModel ProgressBar
        {
            get => progresBar;
            set => SetProperty(ref progresBar, value);
        }
    }

    public class LabelSizeModel
    {
        public Size Value { get; set; }
        public string Name
        {
            get => Utilities.Generic.GetEnumDescription(Value);
        }
    }
}
