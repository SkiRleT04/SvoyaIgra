using SvoyaIgraClient.ViewModels;
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

namespace SvoyaIgraClient.Views
{
    /// <summary>
    /// Логика взаимодействия для UserIdentification.xaml
    /// </summary>
    public partial class UserIdentification : Page
    {
        public UserIdentification()
        {
            InitializeComponent();
            DataContext = new PlayerViewModel();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Register());
        }
    }
}
