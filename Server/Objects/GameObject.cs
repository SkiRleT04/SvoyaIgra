using Core.Enums;
using Core.Objects;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Commands;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Server.Objects
{
    class GameObject
    {

        private Dictionary<string, IEnumerable<Question>> tabeleQuestions;

        public RoomObject Room { get; set; }
        public int CurrentQuestionId { get; set; }
        public Question CurrentQuestion { get; set; }
        public int Points { get; set; }
        
        public Timer SelectQuestionTimer { get; private set; }
        public Timer AnswerButtonClickTimer { get; private set; }
        public Timer AnswerTimer { get; private set; }
        public Dictionary<string, IEnumerable<Question>> TableQuestions
        {
            get
            {
                if (IsEmptyTableQuestions())
                {
                    ThrowRoom();
                    //tabeleQuestions = DB.GetQuestionsTable();
                }
                return tabeleQuestions;
            }
            set => tabeleQuestions = value;
        }

        private int secondAnswer = 0;
        private int secondSelectQuestion = 0;
        private int secondAnswerButtonClick = 0;
        


        private static int SELECT_QUESTION_TIMER = 10;
        private static int ANSWER_BUTTON_CLICK_TIMER = 10;
        private static int ANSWER_TIMER = 10;



        public bool IsEmptyTableQuestions()
        {
            foreach (var item in tabeleQuestions)
            {
                var array = item.Value.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Id != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void RemoveQuestionFromTable(int id)
        {
            foreach (var item in TableQuestions)
            {
                var array = item.Value.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Id == id)
                    {
                        Points = array[i].Points;
                        CurrentQuestion = array[i];
                        array[i].Id = 0;
                        return;
                    }
                }
            }
        }


        public GameObject()
        {

            TableQuestions = new Dictionary<string, IEnumerable<Question>>(DB.GetQuestionsTable());

            AnswerTimer = new Timer();
            AnswerButtonClickTimer = new Timer();
            SelectQuestionTimer = new Timer();
            AnswerTimer.Interval = 1000;
            AnswerButtonClickTimer.Interval =1000;
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
            list.Clear();
            listClickButton.Clear();
            Room.Respondents.Clear();
            Console.WriteLine("Show question");
            var response = new ShowQuestionResponse();
            response.QuestionId = id;
            string packetResponse = JsonConvert.SerializeObject(response);
            Room.SendMessageToAllClients(packetResponse);
            //останавливаем таймер когда игрок выбрал вопрос
            StopSelectQuestionTimer();
            //запускаем таймер для нажания на кнопку "ответ"
            StartAnswerButtonClickTimer();
        }
        

        private void SendUpdateTableCommand()
        {
            var response = new UpdateRoomResponse();
            response.Type = UpdateRoomType.UpdateTable;
            string packetResponse = JsonConvert.SerializeObject(response);
            Room.Respondent = null;
            Room.SendMessageToAllClients(packetResponse);
            //запускаем таймер выбора вопроса при показе таблицы с вопросами
            StartSelectQuestionTimer();
            StopAnswerButtonClickTimer();

        }


        private void ThrowRoom()
        {
            Room.Selector = null;
            Room.Respondent = null;
            CurrentQuestionId = 0;
            CurrentQuestion = null;

            ClientObject[] clients = new ClientObject[Room.Clients.Count];
            Array.Copy(Room.Clients.ToArray(), clients, Room.Clients.Count);


            for (int i = 0; i < clients.Length; i++)
            {
                Room.Server?.Commands[RequestType.RoomLeave].Excecute(clients[i], Room.Server, Room);
            }


            StopAnswerButtonClickTimer();
            StopAnswerTimer();
            StopSelectQuestionTimer();

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
                StopAnswerButtonClickTimer();
                //remove question from table and return on page table
                //RemoveQuestionFromTable(CurrentQuestionId);
                SendUpdateTableCommand();
            }
        }

        private void AnswerTimer_Tick(object sender, ElapsedEventArgs e)
        {
            secondAnswer++;
            if (secondAnswer >= ANSWER_TIMER)
            {
                StopAnswerTimer();
                //chuvachek nepravilno otvetil i zablokirovat knopki otveta
                SendBadAnswerCommand();
            }
        }

      


        public void StopSelectQuestionTimer()
        {
            Console.WriteLine("Таймер выбора вопроса остановлен");
            SelectQuestionTimer.Stop();
        }

        public void StopAnswerButtonClickTimer()
        {
            Console.WriteLine("Таймер кнопки ответа остановлен");
            AnswerButtonClickTimer.Stop();
        }

        public void StopAnswerTimer()
        {
            Console.WriteLine("Таймер ответа остановлен");
            AnswerTimer.Stop();
        }


        public void StartSelectQuestionTimer()
        {
            if (Room.Clients.Count > 0)
            {
                Console.WriteLine("Таймер выбора вопроса запущен");
                secondSelectQuestion = 0;
                SelectQuestionTimer.Start();
            }
        }

        public void StartAnswerButtonClickTimer()
        {
            if (Room.Clients.Count > 0)
            {
                Console.WriteLine("Таймер кнопки ответа запущен");
                secondAnswerButtonClick = 0;
                AnswerButtonClickTimer.Start();
            }
        }

        public void StartAnswerTimer()
        {
            if (Room.Clients.Count > 0)
            {
                Console.WriteLine("Таймер ответа запущен");
                secondAnswer = 0;
                AnswerTimer.Start();
            }
        }


        private Question GetQuestionById(int id)
        {
            foreach (var item in TableQuestions)
            {
                var array = item.Value.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Id == id)
                    {
                        return array[i];
                    }
                }
            }
            return null;
        }


        public List<ClientObject> list = new List<ClientObject>();
        public List<ClientObject> listClickButton = new List<ClientObject>();
        private void SendBadAnswerCommand()
        {

            Room.Game.StopAnswerTimer();
            var response = new CheckAnswerResponse();
            var client = Room.Respondent;

            //var question = GetQuestionById(CurrentQuestionId);
            int points = Points * -1;
            Room.Respondents.Add(client);
            client.UpdatePoints(points);




            foreach (var item in Room.Respondents)
            {
                if (!list.Contains(item) && !listClickButton.Contains(item))
                {
                    var res = new UpdateRoomResponse();
                    res.Type = UpdateRoomType.UpdateTable;

                    Room.SendMessageToDefiniteClient(JsonConvert.SerializeObject(res), item);
                }
                list.Add(item);
            }



            //если все ответили не верно, назначаем селектором первого игрока
            if (Room.Clients.Count == Room.Respondents.Count)
            {
                RemoveQuestionFromTable(CurrentQuestionId);
                if (Room.Clients.Count != 0)
                    Room.Selector = Room.Clients.First();
                Room.Respondents.Clear();
                NotifyPlayersAboutUpdateRoom(client, Room, UpdateRoomType.UpdateTable);
                StopAnswerButtonClickTimer();
            }
            else
            {
                StartAnswerButtonClickTimer();
            }

            if (Room.Respondents.Count != 0)
            {
                //обновляем статус кнопки ответа для всех игроков
                ChangeAnswerButtonPropertyForPlayers(Room);
            }



            string packetResponse = JsonConvert.SerializeObject(response);
            Room.SendMessageToDefiniteClient(packetResponse, client);



            NotifyPlayersAboutUpdateRoom(client, Room, UpdateRoomType.UpdatePlayers);


            

        }




        private void ChangeAnswerButtonPropertyForPlayers(RoomObject room)
        {
            foreach (var client in room.Clients)
            {
                if (!room.Respondents.Contains(client))
                {
                    var response = new BlockAnswerButtonResponse();
                    response.IsEnabled = true;
                    string packetResponse = JsonConvert.SerializeObject(response);
                    room.SendMessageToDefiniteClient(packetResponse, client);
                }
            }
        }

        private void NotifyPlayersAboutUpdateRoom(ClientObject client, RoomObject room, UpdateRoomType type)
        {
            var response = new UpdateRoomResponse();
            if (type == UpdateRoomType.UpdatePlayers)
            {
                response.Type = UpdateRoomType.UpdatePlayers;
                response.Player = client.Player;
                response.Selector = room.Selector.Player;
                if (room.Respondent != null)
                    response.Respondent = room.Respondent.Player;
            }
            else
            {
                response.Type = UpdateRoomType.UpdateTable;
                room.Game.StopAnswerButtonClickTimer();
            }


            //response.Type = UpdateRoomType.NavigateTable;

            //после отправки удаляем респондента
            room.Respondent = null;
            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToAllClients(packetResponse);
        }
    }
}
