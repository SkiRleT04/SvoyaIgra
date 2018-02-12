using Server.Objects.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects
{
    class ServerObject
    {
        static TcpListener tcpListener;
        List<BaseCommand> comands = new List<BaseCommand>();
        List<ClientObject> clients = new List<ClientObject>();
        public ReadOnlyCollection<BaseCommand> Comands => comands.AsReadOnly();



        public ServerObject()
        {
            //initialize comands
            comands.Add(new RegisterUserCommand());
            comands.Add(new LoginUserCommand());
            comands = comands.OrderByDescending(x => x.Frequency).ToList();
            
        }

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client, this);
                    Task.Factory.StartNew(clientObject.Process);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }


        public void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }

        public void RemoveConnection(string id)
        {
            var clientObject = clients.FirstOrDefault(x => x.Id == id);
            clients?.Remove(clientObject);
        }

        public void SendMessageToAllClients(string message)
        {
            foreach (var client in clients)
            {
                if (client.Player != null)
                    client.Writer.WriteLine(message);
            }
        }

        public void SendMessageToAllClientsExceptSendingClient(string message, string idSendingUser)
        {
            foreach (var client in clients)
            {
                if (client.Id != idSendingUser && client.Player!=null)
                    client.Writer.WriteLine(message);
            }
        }

        public void SendMessageToDefiniteClient(string message, string idDefiniteClient)
        {
            foreach (var client in clients)
            {
                if (client.Id == idDefiniteClient)
                {
                    client.Writer.WriteLine(message);
                    break;
                }
            }
        }

        public void Disconnect()
        {
            tcpListener.Stop();
            foreach (var client in clients)
            {
                client.Close();
            }
            Environment.Exit(0);
        }
    }
}
