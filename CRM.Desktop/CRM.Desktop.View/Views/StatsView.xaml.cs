﻿using CRM.Desktop.Data.Models;
using CRM.Desktop.View.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Desktop.View.Views
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    public partial class StatsView : UserControl
    {
        public StatsView(CompanyStatisticsDto dto)
        {
            InitializeComponent();
            this.DataContext = new StatsVM(dto);
        }
    }
}
