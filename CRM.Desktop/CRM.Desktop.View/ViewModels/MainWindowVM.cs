﻿using CRM.Desktop.Data.Helpers;
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
    public class MainWindowVM:BaseVM
    {
        private readonly string _token;
        private UserControl activeControl;
        public UserControl ActiveControl
        {
            get
            {
                return activeControl;
            }
            set
            {
                activeControl = value;
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

        private ICommand changeViewCommand;
        public ICommand ChangeViewCommand
        {
            get
            {
                return changeViewCommand ?? new RelayCommand(ChangeView, true);
            }
        }

        private void ChangeView(object obj)
        {
            if(ActiveControl is UserListView)
            {
                AdministrationData.Users = (ActiveControl.DataContext as UserListVM).UserList.ToList();
            }

            switch (obj.ToString())
            {
                case "Stats":
                    ActiveControl = new StatsView(AdministrationData.Statistics);
                    break;
                case "Users":
                    ActiveControl = new UserListView(AdministrationData.Users,_token);
                    break;
                case "Company":
                    ActiveControl = new CompanyDataView(AdministrationData.Company,_token);
                    break;
            }
        }

        private AdministrationDataDto administrationData;
        public AdministrationDataDto AdministrationData
        {
            get
            {
                return administrationData;
            }
            set
            {
                administrationData = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM(string token)
        {
            _token = token;
            GetDataFromApi(token);
        }

        private void Exit(object arg)
        {
            Environment.Exit(1);
        }

        private async void GetDataFromApi(string token)
        {
            var client = new WebApiClient(token);
            var response = await client.GetAdministrationDataAsync();
            if (response != null && response.Data != null)
            {
                AdministrationData = response.Data;
            }

            if (AdministrationData == null)
            {
                MessageBox.Show("Brak uprawnień do tego modułu.");
                Environment.Exit(1);
            }
        }
    }
}
