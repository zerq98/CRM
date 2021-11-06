using CRM.Desktop.View.ViewModels;
using System.Windows;

namespace CRM.Desktop
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginVM();
        }
    }
}