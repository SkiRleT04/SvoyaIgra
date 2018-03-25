using Client;
using Client.Objects;
using Client.ViewModels;
using Core.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void HideButton(int id)
        {
            Debug.WriteLine("------------");
            for (int i = 1; i < 7; i++)
            {

                var cat = (Layout.FindName("cat" + i) as ItemsControl);
                var buttons = cat.GetChildren<Button>().ToList();

                foreach (var btn in buttons)
                {
                    if ((int)btn.Tag == id)
                        btn.Visibility = Visibility.Hidden;

                    
                    Debug.Write((int)btn.Tag>0?1:0);
                }
                Debug.WriteLine("");
            }
           

        }

        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }

       
           
        



    }
}
