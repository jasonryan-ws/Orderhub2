using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WS.OrderHub.Models;

namespace WS.OrderHub.ViewModels
{
    public class AddressViewModel : ObservableObject
    {
        public readonly AddressModel model;
        public AddressViewModel(AddressModel model)
        {
            this.model = model;
        }

        public Guid Id
        {
            get => model.Id;
            set => SetProperty(model.Id, value, model, (m, p) => m.Id = p);
        }
        public string FirstName
        {
            get => model.FirstName;
            set => SetProperty(model.FirstName, value, model, (m, p) => m.FirstName = p);
        }
        public string MiddleName
        {
            get => model.MiddleName;
            set => SetProperty(model.MiddleName, value, model, (m, p) => m.MiddleName = p);
        }
        public string LastName
        {
            get => model.LastName;
            set => SetProperty(model.LastName, value, model, (m, p) => m.LastName = p);
        }
        public string Company
        {
            get => model.Company;
            set => SetProperty(model.Company, value, model, (m, p) => m.Company = p);
        }
        public string Street1
        {
            get => model.Street1;
            set => SetProperty(model.Street1, value, model, (m, p) => m.Street1 = p);
        }
        public string Street2
        {
            get => model.Street2;
            set => SetProperty(model.Street2, value, model, (m, p) => m.Street2 = p);
        }
        public string Street3
        {
            get => model.Street3;
            set => SetProperty(model.Street3, value, model, (m, p) => m.Street3 = p);
        }
        public string City
        {
            get => model.City;
            set => SetProperty(model.City, value, model, (m, p) => m.City = p);
        }
        public string State
        {
            get => model.State;
            set => SetProperty(model.State, value, model, (m, p) => m.State = p);
        }
        public string PostalCode
        {
            get => model.PostalCode;
            set => SetProperty(model.PostalCode, value, model, (m, p) => m.PostalCode = p);
        }
        public string CountryCode
        {
            get => model.CountryCode;
            set => SetProperty(model.CountryCode, value, model, (m, p) => m.CountryCode = p);
        }
        public string Phone
        {
            get => model.Phone;
            set => SetProperty(model.Phone, value, model, (m, p) => m.Phone = p);
        }
        public string Fax
        {
            get => model.Fax;
            set => SetProperty(model.Fax, value, model, (m, p) => m.Fax = p);
        }
        public string Email
        {
            get => model.Email;
            set => SetProperty(model.Email, value, model, (m, p) => m.Email = p);
        }
        public DateTime DateCreated
        {
            get => model.DateCreated;
            set => SetProperty(model.DateCreated, value, model, (m, p) => m.DateCreated = p);
        }
        public Guid CreatedByNodeId
        {
            get => model.CreatedByNodeId;
            set => SetProperty(model.CreatedByNodeId, value, model, (m, p) => m.CreatedByNodeId = p);
        }
        public DateTime? DateModified
        {
            get => model.DateModified;
            set => SetProperty(model.DateModified, value, model, (m, p) => m.DateModified = p);
        }
        public Guid? ModifiedByNodeId
        {
            get => model.ModifiedByNodeId;
            set => SetProperty(model.ModifiedByNodeId, value, model, (m, p) => m.ModifiedByNodeId = p);
        }


        public string FullName
        {
            get
            {
                var fullName = $"{FirstName} {LastName}" + "\n" +
                    $"{Company}";

                return Regex.Replace(fullName, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            }
        }

        public string FullAddress
        {
            get
            {
                var fullAddress =
                    Street1 + "\n" +
                    Street2 + "\n" +
                    Street3 + "\n" +
                    $"{City}, {State} {PostalCode}" + "\n" +
                    CountryCode;
                return Regex.Replace(fullAddress, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            }
        }


    }
}
