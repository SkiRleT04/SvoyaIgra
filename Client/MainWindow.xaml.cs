using Client;
using SvoyaIgraClient.ViewModels;
using SvoyaIgraClient.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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

namespace SvoyaIgraClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       // PlayerViewModel pvm = new PlayerViewModel();

        public MainWindow()
        {

            InitializeComponent();
            //Auth auth = new Auth();
            //DataContext = pvm;
            Frame.NavigationService.Navigate(new UserIdentification());
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;

            //ClientObject.RecieveMessage();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // pvm.PlayerNickName = "33333333";
        }

        /*private void wMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientObject.Host = File.ReadAllText("config.cfg");
            }
            catch
            {
                MessageBox
            }
            ClientObject.Initialize();
        }*/
    }
}
