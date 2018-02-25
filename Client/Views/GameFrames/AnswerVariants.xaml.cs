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
    /// Логика взаимодействия для AnswerVariants.xaml
    /// </summary>
    public partial class AnswerVariants : Page
    {
        Frame actionFrame { get; set; }
        Grid controls { get; set; }

        public AnswerVariants()
        {
            InitializeComponent();
            DataContext = ClientObject.view as GameViewModel;
        }

        /*
        public AnswerVariants(Frame actionFrame, Grid controls) :this()
        {
            this.actionFrame = actionFrame;
            this.controls = controls;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            actionFrame.NavigationService.GoBack();
            controls.IsEnabled = true;
            
        }*/

        
    }
}
