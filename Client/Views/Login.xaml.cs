using Client;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            //ClientObject.page = this;123
            //if(ClientObject.view!=null)
            ClientObject.view = DataContext as UserViewModel;
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Game());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserIdentification());

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            btnLogIn.IsEnabled = false;
            btnBack.IsEnabled = false;
        }

        private void tbLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnLogIn.IsEnabled = false;
                btnBack.IsEnabled = false;
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnLogIn.IsEnabled = false;
                btnBack.IsEnabled = false;
            }
        }
    }
}
