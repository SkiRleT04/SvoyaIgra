using Client;
using SvoyaIgraClient.Views.GameFrames;
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
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        int count = 0;
        public Game()
        {
            InitializeComponent();
            ActionFrame.NavigationService.Navigate(new Players());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            count++;
            if (count == 1)
            {
                GameFrame.NavigationService.Navigate(new CategoriesAndQuestionsTable());
            }
            else if (count == 2)
            {
                GameFrame.NavigationService.Navigate(new ImageQuestionContent());
            }
            else
            {
                GameFrame.NavigationService.Navigate(new TextQuestionContent());
            }
            if (count == 3)
            {
                count = 0;
            }
        }

        

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            Controls.IsEnabled = false;

            ActionFrame.NavigationService.Navigate(new AnswerVariants(ActionFrame, Controls));



        }

    }
}
