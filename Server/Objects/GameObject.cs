using Core.Enums;
using Core.Objects;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace Server.Objects
{
    class GameObject
    {
        public Dictionary<string, IEnumerable<Question>> tabeleQuestions;
        public ReadOnlyDictionary<string, IEnumerable<Question>> TableQuestions { get; private set; }
        public RoomObject Room { get; set; }
        public int CurrentQuestionId { get; set; }
        
        public Timer SelectQuestionTimer { get; private set; }
        public Timer AnswerButtonClickTimer { get; private set; }
        public Timer AnswerTimer { get; private set; }



        private int secondAnswer = 0;
        private int secondSelectQuestion = 0;
        private int secondAnswerButtonClick = 0;
        


        private static int SELECT_QUESTION_TIMER = 10;
        private static int ANSWER_BUTTON_CLICK_TIMER = 10;
        private static int ANSWER_TIMER = 10;


   

        public void RemoveQuestionFromTable(int id)
        {
            foreach (var item in TableQuestions)
            {
                var array = item.Value.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Id == id)
                    {
                        array[i].Id = 0;
                        return;
                    }
                }
            }
        }


        public GameObject()
        {
            tabeleQuestions = DB.GetQuestionsTable();
            TableQuestions = new ReadOnlyDictionary<string, IEnumerable<Question>>(tabeleQuestions);

            AnswerTimer = new Timer();
            AnswerButtonClickTimer = new Timer();
            SelectQuestionTimer = new Timer();
            AnswerTimer.Interval = 1000;
            AnswerButtonClickTimer.Interval = 1000;
            SelectQuestionTimer.Interval = 1000;

            AnswerTimer.Elapsed += AnswerTimer_Tick;
            AnswerButtonClickTimer.Elapsed += AnswerButtonClickTimer_Tick;
            SelectQuestionTimer.Elapsed += SelectQuestionTimer_Tick;
        }

        private void SelectQuestionTimer_Tick(object sender, ElapsedEventArgs e)
        {
            secondSelectQuestion++;
            if (secondSelectQuestion >= SELECT_QUESTION_TIMER)
            {
                StopSelectQuestionTimer();
                //send random question
                var question = GetFirstQuestion();
                if (question != null)
                {
                    SendShowQuestionCommand(question.Id);
                    RemoveQuestionFromTable(question.Id);
                }
            }
        }

        private void SendShowQuestionCommand(int id)
        {

            Console.WriteLine("Show question");
            var response = new ShowQuestionResponse();
            response.QuestionId = id;
            string packetResponse = JsonConvert.SerializeObject(response);
            Room.SendMessageToAllClients(packetResponse);
            //запускаем таймер для нажания на кнопку "ответ"
            AnswerButtonClickTimer.Start();
        }

        private void SendUpdateTableCommand()
        {
            var response = new UpdateRoomResponse();
            response.Type = UpdateRoomType.UpdateTable;
            string packetResponse = JsonConvert.SerializeObject(response);
            Room.Respondent = null;
            Room.SendMessageToAllClients(packetResponse);
            //запускаем таймер выбора вопроса при показе таблицы с вопросами
            SelectQuestionTimer.Start();
        }

        private Question GetFirstQuestion()
        {
            foreach (var item in TableQuestions)
            {
                var array = item.Value.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Id != 0)
                    {
                        return array[i];
                    }
                }
            }
            return null;
        }

        private void AnswerButtonClickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            secondAnswerButtonClick++;
            if (secondAnswerButtonClick >= ANSWER_BUTTON_CLICK_TIMER)
            {
                AnswerButtonClickTimer.Stop();
                //remove question from table and return on page table
                RemoveQuestionFromTable(CurrentQuestionId);
                SendUpdateTableCommand();
            }
        }

        private void AnswerTimer_Tick(object sender, ElapsedEventArgs e)
        {
            secondAnswer++;
            if (secondAnswer >= ANSWER_TIMER)
            {
                AnswerButtonClickTimer.Stop();
                //chuvachek nepravilno otvetil
            }
        }

      


        public void StopSelectQuestionTimer()
        {
            SelectQuestionTimer.Stop();
            secondSelectQuestion = 0;
        }

        public void StopAnswerButtonClickTimer()
        {
            AnswerButtonClickTimer.Stop();
            secondAnswerButtonClick = 0;
        }






    }
}
