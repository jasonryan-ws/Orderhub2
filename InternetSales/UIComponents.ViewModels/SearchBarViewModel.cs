using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIComponents.ViewModels
{
    public class SearchBarViewModel : ObservableObject
    {
        public Action Submit;
        public Action SelectAll;
        public Action KeywordChanged;

        private string keyword;
        public string Keyword
        {
            get => keyword;
            set
            {
                if (SetProperty(ref keyword, value) && KeywordChanged != null)
                    KeywordChanged();
            }
        }

        private string hint = "Search";
        public string Hint
        {
            get => hint;
            set => SetProperty(ref hint, value);
        }

        private string leadingIcon = "Search";
        public string LeadingIcon
        {
            get => leadingIcon;
            set => SetProperty(ref leadingIcon, value);
        }

        private bool hasLeadingIcon = true;
        public bool HasLeadingIcon
        {
            get => hasLeadingIcon;
            set => SetProperty(ref hasLeadingIcon, value);
        }

        private bool hasClearButton = true;
        public bool HasClearButton
        { 
            get => hasClearButton;
            set => SetProperty(ref hasClearButton, value);
        }

        public ICommand SubmitCommand => new RelayCommand(ExecuteSubmit);
        private void ExecuteSubmit()
        {
            if (Submit != null)
                Submit();
            if (SelectAll != null)
                SelectAll();
        }


    }
}
