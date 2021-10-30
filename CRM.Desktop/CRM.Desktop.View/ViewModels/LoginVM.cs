using CRM.Desktop.Data.Helpers;
using CRM.Desktop.Data.Models;
using CRM.Desktop.View.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CRM.Desktop.View.ViewModels
{
    public class LoginVM : BaseVM
    {
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
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

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? new RelayCommand(TryLoginAsync, true);
            }
        }

        private void Exit(object arg)
        {
            Environment.Exit(1);
        }

        private async void TryLoginAsync(object arg)
        {
            var client = new WebApiClient();

            var response = await client.LoginAsync(new LoginDto
            {
                Login = Username,
                Password = Password
            });

            if(response!=null && response.Code == 200)
            {
                MainWindow window = new MainWindow(response.Data.Token);
                window.Show();
                (arg as Window).Close();
            }
        }
    }
}
