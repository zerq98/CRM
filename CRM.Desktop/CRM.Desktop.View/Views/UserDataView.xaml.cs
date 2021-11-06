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
using System.Windows.Shapes;

namespace CRM.Desktop.View.Views
{
    /// <summary>
    /// Interaction logic for UserDataView.xaml
    /// </summary>
    public partial class UserDataView : Window
    {
        public UserData returnUser;
        public UserDataView(string token, string id)
        {
            InitializeComponent();
            this.DataContext = new UserDataVM(token, id);
        }
    }
}
