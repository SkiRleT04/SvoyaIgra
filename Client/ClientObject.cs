using Client.Objects.Commands;
using Core.Packets.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using DevExpress.Mvvm;
using Core.Objects;

namespace Client
{
    static class ClientObject
    {

        public static string Host
        {
            get
            {
                try
                {
                    return File.ReadAllText("config.cfg");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.Current.MainWindow.Title);
                    Application.Current.Shutdown();
                }
                return "";
            }
        }
        private const int port = 8888;
        static TcpClient client;
        static StreamReader reader;
        static StreamWriter writer;
        public static ViewModelBase view;
        public static User user;

        public static bool isConnected()
        {
            try
            {
                return client.Connected;
            }
            catch
            {
                Environment.Exit(0);
            }
            return false;
        }

        static ClientObject()
        {
            try
            {
                client = new TcpClient();
                client.Connect(Host, port);
                writer = new StreamWriter(client.GetStream());
                reader = new StreamReader(client.GetStream());
                RecieveMessage();
            }
            catch
            {
                MessageBox.Show("Сервер временно недоступен...", "Ошибка подключения");
                Environment.Exit(0);
            }
        }

        public static void SendMessage(string s)
        {
            try
            {
                writer.WriteLine(s);
                writer.Flush();
            }
            catch
            {
                MessageBox.Show("Сервер временно недоступен...", "Ошибка подключения");
                Environment.Exit(0);
            }
        }

        public static void RecieveMessage()
        {
            try
            {
                List<BaseCommand> commands = new List<BaseCommand>();
                commands.Add(new CheckAnswerCommand());
                commands.Add(new GetRoomInfoCommand());
                commands.Add(new GetWinnerCommand());
                commands.Add(new LoginUserCommand());
                commands.Add(new RegisterUserCommand());
                commands.Add(new SetRespondentCommand());
                commands.Add(new RoomJoinCommand());
                commands.Add(new RoomLeaveCommand());
                commands.Add(new UpdateRoomCommand());
                commands.Add(new ShowQuestionCommand());
                commands.Add(new BlockAnswerButtonCommand());
                commands.Add(new RemoveQuestionCommand());


                commands = commands.OrderByDescending(x => x.Frequency).ToList();

                Task.Run(() =>
                {
                    try
                    {


                        while (true)
                        {
                            string s = reader.ReadLine();

                            if (!String.IsNullOrEmpty(s))
                            {
                                BaseResponse baseResponse = JsonConvert.DeserializeObject<BaseResponse>(s);

                                foreach (BaseCommand command in commands)
                                {
                                    if (command.TypesAreEqual(baseResponse.Request))
                                    {
                                        command.Execute(s);
                                    }
                                }
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Сервер временно недоступен...", "Ошибка подключения");
                        Environment.Exit(0);

                    }
                });
            }
            catch
            {
                MessageBox.Show("Сервер временно недоступен...", "Ошибка подключения");
                Environment.Exit(0);
            }

        }

        static void Disconnect()
        {

            reader?.Close();
            writer?.Close();
            client?.Close();
        }
    }
}
