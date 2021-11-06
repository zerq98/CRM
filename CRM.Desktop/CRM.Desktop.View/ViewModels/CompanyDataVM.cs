using CRM.Desktop.Data.Helpers;
using CRM.Desktop.Data.Models;
using CRM.Desktop.View.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRM.Desktop.View.ViewModels
{
    public class CompanyDataVM : BaseVM
    {
        private CompanyDataDto company;
        public CompanyDataDto Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
                OnPropertyChanged();
            }
        }

        private List<ComboBoxItem> provinceList;
        public List<ComboBoxItem> ProvinceList
        {
            get
            {
                return provinceList;
            }
            set
            {
                provinceList = value;
                OnPropertyChanged();
            }
        }

        public ComboBoxItem Province
        {
            get
            {
                if (Company != null)
                {
                    return ProvinceList.FirstOrDefault(x => x.Content.ToString() == Company.Address.Province);
                }
                else
                {
                    return new ComboBoxItem { Content = "" };
                }
            }
            set
            {
                Company.Address.Province = (value as ComboBoxItem).Content.ToString();
                OnPropertyChanged();
            }
        }

        private ICommand saveCommand;
        private readonly string _token;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? new RelayCommand(SaveData, true);
            }
        }

        private async void SaveData(object arg)
        {
            var client = new WebApiClient(_token);
            var response = await client.UpdateCompanyDataAsync(Company);

            if (response.Code == 201)
            {
                (arg as CompanyDataView).companyData = Company;
            }
            else
            {
                MessageBox.Show(response.ErrorMessage);
            }
        }

        public CompanyDataVM(CompanyDataDto dto,string token)
        {
            _token = token;
            Company = dto;

            ProvinceList = new List<ComboBoxItem>
            {
                    new ComboBoxItem
                    {
                        Content="dolnośląskie"
                    },
                    new ComboBoxItem
                    {
                        Content="kujawsko-pomorskie"
                    },
                    new ComboBoxItem
                    {
                        Content="lubelskie"
                    },
                    new ComboBoxItem
                    {
                        Content="lubuskie"
                    },
                    new ComboBoxItem
                    {
                        Content="łódzkie"
                    },
                    new ComboBoxItem
                    {
                        Content="małopolskie"
                    },
                    new ComboBoxItem
                    {
                        Content="mazowieckie"
                    },
                    new ComboBoxItem
                    {
                        Content="opolskie"
                    },
                    new ComboBoxItem
                    {
                        Content="podkarpackie"
                    },
                    new ComboBoxItem
                    {
                        Content="podlaskie"
                    },
                    new ComboBoxItem
                    {
                        Content="pomorskie"
                    },
                    new ComboBoxItem
                    {
                        Content="śląskie"
                    },
                    new ComboBoxItem
                    {
                        Content="świętokrzyskie"
                    },
                    new ComboBoxItem
                    {
                        Content="warmińsko-mazurskie"
                    },
                    new ComboBoxItem
                    {
                        Content="wielkopolskie"
                    },
                    new ComboBoxItem
                    {
                        Content="zachodniopomorskie"
                    }
                };
            OnPropertyChanged("Province");
        }
    }
}
