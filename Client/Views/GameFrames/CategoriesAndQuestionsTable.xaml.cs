using Client;
using Client.ViewModels;
using Core.Objects;
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
    /// Логика взаимодействия для CategoriesAndQuestionsTable.xaml
    /// </summary>
    public partial class CategoriesAndQuestionsTable : Page
    {
        public CategoriesAndQuestionsTable()
        {
            InitializeComponent();
            
            DataContext = ClientObject.view as GameViewModel;
            
            /*foreach (var item in Questions.Children)
            {
                if (item.GetType() == typeof(Button))
                {
                    
                    MessageBox.Show(((Button)item).Content.ToString());
                }
            }*/



        }

        public void ChangeButtonEProp(bool isEnabled)
        {
            for (int i = 1; i < 7; i++)
                (Layout.FindName("cat"+i) as ItemsControl).IsEnabled = isEnabled;
        }

    }
}
