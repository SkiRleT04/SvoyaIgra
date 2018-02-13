using Core.Enums;
using Core.Objects;
using Core.Packets.Request;
using Newtonsoft.Json;
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

namespace TestClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientObject client;
        public MainWindow()
        {
            InitializeComponent();
            client = new ClientObject();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            /*var request = new RegisterUserRequest();
            request.User = new User("skirlet", "qwerty12");
                        string json = JsonConvert.SerializeObject(request);
            */

            var request = new GetRoomRequest();
            string json = JsonConvert.SerializeObject(request);


            client.SendMessage(json);
        }

        
    }
}
