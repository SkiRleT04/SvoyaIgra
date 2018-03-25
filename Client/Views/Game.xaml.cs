using Client;
using Client.ViewModels;
using SvoyaIgraClient.Views.GameFrames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        public Timer selectQuestionTimer { get; private set; }
        public Timer answerButtonClickTimer { get; private set; }
        public Timer answerTimer { get; private set; }


        private int secondAnswer = 10;
        private int secondSelectQuestion = 10;
        private int secondAnswerButtonClick = 10;


      

        private void SelectQuestionTimer_Tick(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                secondSelectQuestion--;
                SelectQuestionTimer.Content = $"Выбор вопроса: {secondSelectQuestion} сек.";
                if (secondSelectQuestion <= 0)
                {
                    StopSelectQuestionTimer();
                }
            });
        }

        private void AnswerButtonClickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                secondAnswerButtonClick--;
                AnswerButtonClickTimer.Content = $"Возможность ответить: {secondAnswerButtonClick} сек.";
                if (secondAnswerButtonClick <= 0)
                {
                    StopAnswerButtonClickTimer();

                }

            });
        }

        private void AnswerTimer_Tick(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                secondAnswer--;
                AnswerTimer.Content = $"Ответ на вопрос: {secondAnswer} сек.";
                if (secondAnswer <= 0)
                {
                    StopAnswerTimer();
                }

            });
            }

        public void StopSelectQuestionTimer()
        {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SelectQuestionTimer.Content = "Выбор вопроса";
            selectQuestionTimer.Stop();
                    });
                }

        public void StopAnswerButtonClickTimer()
        {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            AnswerButtonClickTimer.Content = "Возможность ответить";

            answerButtonClickTimer.Stop();
                        });
                    }

        public void StopAnswerTimer()
        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                AnswerTimer.Content = "Ответ на вопрос";

                                answerTimer.Stop();
                            });
        }


        public void StartSelectQuestionTimer()
        {
            
            secondSelectQuestion = 10;
            selectQuestionTimer.Start();
        }

        public void StartAnswerButtonClickTimer()
        {
            
            secondAnswerButtonClick = 10;
            answerButtonClickTimer.Start();
        }

        public void StartAnswerTimer()
        {
            secondAnswer = 10;
            answerTimer.Start();
        }



        int count = 0;
        public Game()
        {
           
            InitializeComponent();
            DataContext = new GameViewModel();
            ClientObject.view = DataContext as GameViewModel;
            ActionFrame.NavigationService.Navigate(new Players());
            GameFrame.NavigationService.Navigate(new CategoriesAndQuestionsTable());
            //ClientObject.view = DataContext as GameViewModel;
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;

            answerTimer = new Timer();
            answerButtonClickTimer = new Timer();
            selectQuestionTimer = new Timer();
           answerTimer.Interval = 1000;
            answerButtonClickTimer.Interval = 1000;
            selectQuestionTimer.Interval = 1000;

            answerTimer.Elapsed += AnswerTimer_Tick;
           answerButtonClickTimer.Elapsed += AnswerButtonClickTimer_Tick;
            selectQuestionTimer.Elapsed += SelectQuestionTimer_Tick;

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
            btnAnswer.IsEnabled = false;
        }

        private void GameFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if(e.Content as CategoriesAndQuestionsTable != null)
            {
                StartSelectQuestionTimer();
                StopAnswerButtonClickTimer();
                StopAnswerTimer();
            }



            if (e.Content as TextQuestionContent!=null || e.Content as ImageQuestionContent != null)
            {
                StopSelectQuestionTimer();
                StopAnswerTimer();
                StartAnswerButtonClickTimer();
            }
        }

        private void ActionFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Content as AnswerVariants != null)
            {
                StartAnswerTimer();
                StopAnswerButtonClickTimer();
                StopSelectQuestionTimer();
            }

            
        }
    }
}
