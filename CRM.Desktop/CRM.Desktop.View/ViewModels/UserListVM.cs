using CRM.Desktop.Data.Helpers;
using CRM.Desktop.Data.Models;
using CRM.Desktop.View.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRM.Desktop.View.ViewModels
{
    public class UserListVM : BaseVM
    {
        private ObservableCollection<UserForAdministrationDto> userList;
        public ObservableCollection<UserForAdministrationDto> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged();
            }
        }

        private ICommand showDetailCommand;
        private readonly string _token;

        public ICommand ShowDetailCommand
        {
            get
            {
                return showDetailCommand ?? new RelayCommand(OpenUserDetails,true);
            }
        }

        private void OpenUserDetails(object obj)
        {
            UserDataView userData = new UserDataView(_token, obj.ToString());
            if (userData.ShowDialog().Value)
            {
                if (obj.ToString() != "")
                {
                    for(int i=0;i<UserList.Count;i++)
                    {
                        if (UserList[i].Id == userData.returnUser.Id)
                        {
                            UserList[i].Department = userData.returnUser.Department;
                            UserList[i].Gender = userData.returnUser.Gender;
                            UserList[i].Name = userData.returnUser.FirstName + " " + userData.returnUser.LastName;
                        }
                    }
                }
                else
                {
                    UserList.Insert(UserList.Count - 1, new UserForAdministrationDto
                    {
                        Id = userData.returnUser.Id,
                        Department = userData.returnUser.Department,
                        Gender = userData.returnUser.Gender,
                        Name = userData.returnUser.FirstName + " " + userData.returnUser.LastName,
                        CanDelete = true,
                        StartDate = DateTime.Now.ToShortDateString()
                    });
                }
            }
        }

        public UserListVM(List<UserForAdministrationDto> users,string token)
        {
            _token = token;
            if(users.FirstOrDefault(x=>x.Name=="Dodaj użytkownika")==null)
            {
                users.Add(new UserForAdministrationDto
                {
                    Id = "",
                    Gender = false,
                    StartDate = "",
                    CanDelete = false,
                    Department = "",
                    Name = "Dodaj użytkownika"
                });
            }
            UserList = new ObservableCollection<UserForAdministrationDto>(users);
        }
    }
}
