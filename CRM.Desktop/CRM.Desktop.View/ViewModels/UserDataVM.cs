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
    public class UserDataVM : BaseVM
    {
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public bool UserAdding
        {
            get
            {
                return User!=null? User.Permissions.FirstOrDefault(x => x.Name == "Dodawanie użytkowników").Selected : false;
            }
            set
            {
                foreach(var permission in User.Permissions)
                {
                    if(permission.Name=="Dodawanie użytkowników")
                    {
                        permission.Selected=value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool UserRemoving
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Usuwanie użytkowników").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Usuwanie użytkowników")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool AdminPanel
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Panel administracji").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Panel administracji")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool LeadList
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Przeglądanie leadów").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Przeglądanie leadów")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool LeadModification
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Modyfikacja cudzych leadów").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Modyfikacja cudzych leadów")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool OppoList
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Przeglądanie szans sprzedaży").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Przeglądanie szans sprzedaży")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool OppoModification
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Modyfikacja cudzych szans sprzedaży").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Modyfikacja cudzych szans sprzedaży")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool ProductList
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Przeglądanie produktów").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Przeglądanie produktów")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        public bool ProductModification
        {
            get
            {
                return User != null ? User.Permissions.FirstOrDefault(x => x.Name == "Modyfikacja produktów").Selected : false;
            }
            set
            {
                foreach (var permission in User.Permissions)
                {
                    if (permission.Name == "Modyfikacja produktów")
                    {
                        permission.Selected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        private UserData user;
        public UserData User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
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
                if (user != null)
                {
                    return ProvinceList.FirstOrDefault(x=>x.Content.ToString()==User.Address.Province);
                }
                else
                {
                    return new ComboBoxItem { Content = "" };
                }
            }
            set
            {
                User.Address.Province = (value as ComboBoxItem).Content.ToString();
                OnPropertyChanged();
            }
        }

        private List<ComboBoxItem> genderList;
        public List<ComboBoxItem> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged();
            }
        }

        public ComboBoxItem Gender
        {
            get
            {
                if (user != null)
                {
                    return !User.Gender ? GenderList[0] : GenderList[1];
                }
                else
                {
                    return new ComboBoxItem { Content = "" };
                }
            }
            set
            {
                User.Gender = (value as ComboBoxItem).Content.ToString() != "Mężczyzna";
                OnPropertyChanged();
            }
        }
        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ?? new RelayCommand(Exit, true);
            }
        }

        private ICommand saveCommand;
        private readonly string _token;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? new RelayCommand(SaveUser, true);
            }
        }

        private async void SaveUser(object arg)
        {
            var client = new WebApiClient(_token);
            var response = await client.UpsertUserAsync(User);

            if (response.Code == 201)
            {
                User.Id = response.Data.ToString();
                (arg as UserDataView).returnUser = User;
                (arg as UserDataView).DialogResult = true;
                (arg as UserDataView).Close();
            }
            else
            {
                MessageBox.Show(response.ErrorMessage);
            }
        }

        public UserDataVM(string token, string id)
        {
            _token = token;
            if (id == "")
            {
                Title = "Dodaj użytkownika";
            }
            else
            {
                Title = "";
            }

            GetUserDataAsync(token, id);
        }

        private async void GetUserDataAsync(string token, string id)
        {
            var client = new WebApiClient(token);
            var response = await client.GetUserDataAsync(id);

            if (response.Data != null)
            {
                User = response.Data;
            }
            else
            {
                User = new UserData
                {
                    Address = new AddressDto
                    {
                        Id = 0,
                        Street = "",
                        Province = "",
                        ApartmentNumber = "",
                        City = "",
                        HouseNumber = "",
                        PostCode = ""
                    },
                    Id = "",
                    Department = "",
                    Email = "",
                    FirstName = "",
                    Gender = false,
                    LastName = "",
                    Login = "",
                    Password = "",
                    Permissions = new List<PermissionDto>
                    {
                        new PermissionDto
                        {
                            Name="Dodawanie użytkowników",
                            Selected = false
                        },
                        new PermissionDto
                        {
                            Name="Usuwanie użytkowników",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Panel administracji",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Przeglądanie leadów",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Modyfikacja cudzych leadów",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Przeglądanie szans sprzedaży",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Modyfikacja cudzych szans sprzedaży",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Przeglądanie szans sprzedaży",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Przeglądanie produktów",
                            Selected=false
                        },
                        new PermissionDto
                        {
                            Name="Modyfikacja produktów",
                            Selected=false
                        }
                    },
                    PhoneNumber = ""
                };
            }

            GenderList = new List<ComboBoxItem>
                {
                    new ComboBoxItem
                    {
                        Content="Mężczyzna"
                    },
                    new ComboBoxItem
                    {
                        Content="Kobieta"
                    }
                };

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
            OnPropertyChanged("Gender");
            OnPropertyChanged("Province");
            OnPropertyChanged("UserAdding");
            OnPropertyChanged("UserRemoving");
            OnPropertyChanged("AdminPanel");
            OnPropertyChanged("LeadList");
            OnPropertyChanged("LeadModification");
            OnPropertyChanged("OppoList");
            OnPropertyChanged("OppoModification");
            OnPropertyChanged("ProductList");
            OnPropertyChanged("ProductModification");
        }
        private void Exit(object arg)
        {
            (arg as UserDataView).DialogResult = false;
            (arg as UserDataView).Close();
        }
    }
}
