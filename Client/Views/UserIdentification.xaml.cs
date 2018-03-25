using Client;
using DevExpress.Mvvm.UI.Native;
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
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(ClientObject.isConnected())
            this.NavigationService.Navigate(new Login());
            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ClientObject.isConnected())
                this.NavigationService.Navigate(new Register());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
