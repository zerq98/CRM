using CRM.Desktop.Data.Models;
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
    /// Interaction logic for CompanyDataView.xaml
    /// </summary>
    public partial class CompanyDataView : UserControl
    {
        public CompanyDataDto companyData;
        public CompanyDataView(CompanyDataDto dto,string token)
        {
            InitializeComponent();
            this.DataContext = new CompanyDataVM(dto,token);
        }
    }
}
