using Data;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIComponents.ViewModels.Modules;

namespace UIComponents.ViewModels
{
    public class DatabaseEntryViewModel : ObservableObject
    {
        public Action Submit;
        public Action Reset;
        public Action Close;
        public DatabaseEntryViewModel() { }
        public DatabaseEntryViewModel(bool showNameField = false)
        {
            NameFieldVisibility = showNameField ? "Visible" : "Collapsed";
        }

        private ProgressBarViewModel progressBar = new ProgressBarViewModel();
        public ProgressBarViewModel ProgressBar
        {
            get => progressBar;
            set => SetProperty(ref progressBar, value);
        }

        private BannerViewModel pageBanner = new BannerViewModel();
        public BannerViewModel PageBanner
        {
            get => pageBanner;
            set => SetProperty(ref pageBanner, value);
        }

        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (SetProperty(ref isOpen, value) && value)
                    ExecuteReset();
            }
        }

        private string nameFieldVisibility;
        public string NameFieldVisibility
        {
            get => nameFieldVisibility;
            set => SetProperty(ref nameFieldVisibility, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (SetProperty(ref name, value))
                {
                    ValueChanged();
                }
            }
        }

        private bool isIntegrated;
        public bool IsIntegrated
        {
            get => isIntegrated;
            set
            {
                if (SetProperty(ref isIntegrated, value))
                {
                    CredentialVisibility = value ? "Collapsed" : "Visible";
                    ValueChanged();
                }
            }
        }

        private string credentialVisibility = "Visible";
        public string CredentialVisibility
        {
            get => credentialVisibility;
            set => SetProperty(ref credentialVisibility, value);
        }

        private string server;
        public string Server
        {
            get => server;
            set
            {
                if (SetProperty(ref server, value))
                {
                    ValueChanged();
                }
            }
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                if (SetProperty(ref username, value))
                {
                    ValueChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (SetProperty(ref password, value))
                {
                    ValueChanged();
                }
            }
        }

        private string database;
        public string Database
        {
            get => database;
            set
            {
                if (SetProperty(ref database, value))
                {
                    ValueChanged(false);
                }
            }
        }

        private bool submitButtonIsEnabled;
        public bool SubmitButtonIsEnabled
        {
            get => submitButtonIsEnabled;
            set => SetProperty(ref submitButtonIsEnabled, value);
        }

        private void ValueChanged(bool clearList = true)
        {
            if (clearList)
                DatabaseList = null;
            PageBanner.Close();
            SubmitButtonIsEnabled =
                !string.IsNullOrWhiteSpace(Name) &&
                !string.IsNullOrWhiteSpace(Server) &&
                !string.IsNullOrWhiteSpace(Database) &&
                (IsIntegrated || (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password)));
        }

        private IEnumerable<string> databaseList;
        public IEnumerable<string> DatabaseList
        {
            get => databaseList;
            set => SetProperty(ref databaseList, value);
        }

        public ICommand SubmitCommand => new RelayCommand(ExecuteSubmit);
        private void ExecuteSubmit()
        {
            Submit();
        }

        public ICommand CloseCommand => new RelayCommand(ExecuteClose);
        private void ExecuteClose()
        {
            Close();
        }

        private bool isDropDownOpen;
        public bool IsDropDownOpen
        {
            get => isDropDownOpen;
            set => SetProperty(ref isDropDownOpen, value);
        }

        public ICommand ResetCommand => new RelayCommand(ExecuteReset);

        public ICommand RefreshCommand => new RelayCommand(ExecuteRefresh);
        private async void ExecuteRefresh()
        {
            await Task.Run(() =>
            {
                PageBanner.Close();
                try
                {
                    ProgressBar.Wait();
                    string connectionString;
                    if (IsIntegrated)
                        connectionString = Utilities.Builder.SQLConnectionString(Server);
                    else
                        connectionString = Utilities.Builder.SQLConnectionString(Server, Username, Password);
                    var list = SQL.GetDatabases(connectionString);
                    if (list.Count > 0)
                    {
                        DatabaseList = list;
                        Database = list[0];
                        IsDropDownOpen = true;
                    }
                    else
                    {
                        throw new Exception("This server does not contain any database catalogs");
                    }
                }
                catch (Exception ex)
                {

                    PageBanner.Show(ex.Message, MessageType.Danger, false);
                }
                finally
                {
                    ProgressBar.Collapse();
                }
            });
        }

        private void ExecuteReset()
        {
            PageBanner.Close();
            if (Reset == null)
                Clear();
            else
                Reset();

        }

        public void Clear()
        {
            PageBanner.Close();
            Name = null;
            Server = null;
            Username = null;
            Password = null;
            DatabaseList = null;
            Database = null;
        }
    }
}
