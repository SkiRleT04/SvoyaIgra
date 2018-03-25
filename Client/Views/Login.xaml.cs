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
            if(ClientObject.view!=null)
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
    }
}
