using Client;
using SvoyaIgraClient.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            ClientObject.view = DataContext as UserViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Game());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserIdentification());

        }

        
    }
}
