using Client;
using Client.ViewModels;
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

namespace SvoyaIgraClient.Views.GameFrames
{
    /// <summary>
    /// Логика взаимодействия для ImageQuestionContent.xaml
    /// </summary>
    public partial class ImageQuestionContent : Page
    {
        public ImageQuestionContent()
        {
            InitializeComponent();
            DataContext = ClientObject.view as GameViewModel;

        }
    }
}
