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
        
        private const string host = "192.168.0.109";
        private const int port = 8888;
        static TcpClient client;
        static StreamReader reader;
        static StreamWriter writer;
        public static Page page;
        public static ViewModelBase view;
        public static User user;

        static ClientObject()
        {
            client = new TcpClient();
            client.Connect(host, port);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
            RecieveMessage();
        }

        public static void SendMessage(string s)
        {
            writer.WriteLine(s);
            writer.Flush();
        }

        public static void RecieveMessage()
        {
            List<BaseCommand> commands = new List<BaseCommand>();
            commands.Add(new CheckAnswerCommand());
            commands.Add(new GetQuestionsTableCommand());
            commands.Add(new GetWinnerCommand());
            commands.Add(new LoginUserCommand());
            commands.Add(new RegisterUserCommand());
            commands.Add(new SetRespondentCommand());
            commands = commands.OrderByDescending(x => x.Frequency).ToList();

            Task.Run(() =>{
                while (true)
                {
                   string s = reader.ReadLine();

                   if(!String.IsNullOrEmpty(s))
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
            });
            
        }

        static void Disconnect()
        {
           
            reader?.Close();
            writer?.Close();
            client?.Close();
        }
    }
}
