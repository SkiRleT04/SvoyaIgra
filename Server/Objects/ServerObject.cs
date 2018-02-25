using Core.Enums;
using Core.Objects;
using Server.Objects.Commands;
using Server.Objects.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server.Objects
{
    class ServerObject
    {
        static object locker = new Object();
        static TcpListener tcpListener;
        List<RoomObject> rooms = new List<RoomObject>();
        public ReadOnlyCollection<RoomObject> Rooms => rooms.AsReadOnly();
        IDictionary<RequestType, ICommand> commands = new Dictionary<RequestType, ICommand>();
        public ReadOnlyDictionary<RequestType, ICommand> Commands { get; }
        List<ClientObject> tmpClients = new List<ClientObject>();
        public ReadOnlyCollection<ClientObject> TmpClients => tmpClients.AsReadOnly();


        //инициализация сервера
        public ServerObject()
        {
            //initialize comands
            commands.Add(RequestType.LoginUser, new LoginUserCommand());
            commands.Add(RequestType.RegisterUser, new RegisterUserCommand());
            commands.Add(RequestType.GetRooms, new GetRoomsCommand());
            commands.Add(RequestType.RoomJoin, new RoomJoinCommand());
            commands.Add(RequestType.RoomLeave, new RoomLeaveCommand());
            commands.Add(RequestType.GetRoomInfo, new GetRoomInfoCommand());
            commands.Add(RequestType.CheckAnswer, new CheckAnswerCommand());
            commands.Add(RequestType.ShowQuestion, new ShowQuestionCommand());
            Commands = new ReadOnlyDictionary<RequestType, ICommand>(commands);
        }

        //обновление комнат/
        private void UpdateRooms()
        {
            lock (locker)
            {
                if (rooms.Count == 0)
                {
                    rooms.Add(new RoomObject(1, NameRoom.Get(), 3));
                }
            }
            //если пустых комнат больше чем одна, оставляем только одну
            var emptyRooms = rooms.Where(r => r.Info.PlayersCount == 0).ToArray();
            if (emptyRooms.Length > 1)
            {
                for (int i = 0; i < emptyRooms.Length - 1; i++)
                    rooms.Remove(emptyRooms[i]);
                return;
            }

            //если в комнате нету свободных мест, добавляем новую
            var freeRooms = rooms.Where(r => r.Info.PlayersCount < r.Info.Size).ToArray();
            if (freeRooms.Length == 0)
            {
                int idRoom = rooms.OrderByDescending(r => r.Info.Id).First().Info.Id + 1;
                rooms.Add(new RoomObject(idRoom, NameRoom.Get(), 3));
            } 
        }
        
        //получение комнаты по ид
        public RoomObject GetRoomById(int id)
        {
            return rooms.FirstOrDefault(r => r.Info.Id == id);
        }

        //получение свободных комнат
        public List<Room> GetFreeRooms()
        {
            UpdateRooms();
            return rooms.Where(x => x.Info.PlayersCount < x.Info.Size).Select(x => x.Info).ToList();
        }
       
        //прослушивание новых подключений
        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("The server is started. Waiting for connections...");
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
        
        //отправка сообщения клиенту
        public void SendMessageToDefiniteClient(string message, ClientObject clientObject)
        {
            foreach (var client in tmpClients)
            {
                if (client.Id == clientObject.Id)
                {
                    client.Writer.WriteLine(message);
                    break;
                }
            }
        }

        //отправка сообщения всем временным клиентам
        public void SendMessageToAllClients(string message)
        {
            foreach (var client in tmpClients)
            {
                client.Writer.WriteLine(message);
            }
        }

        public void SendMessageToAllAuthClients(string message)
        {
            foreach (var client in tmpClients)
            {
                if (client.Player != null)
                    client.Writer.WriteLine(message);
            }

        }

        //добавляет клиента во временные подключения
        public void AddConnection(ClientObject clientObject)
        {
            tmpClients.Add(clientObject);
        }
        
        //проверяет временный клиент или нет
        public bool IsTempClient(ClientObject clientObject)
        {
            return tmpClients.FirstOrDefault(u => u.Id == clientObject.Id) != null;
        }
       
        //удаляет клиента из временного подключения
        public void RemoveConnection(ClientObject clientObject)
        {
            tmpClients.Remove(clientObject);
        }
        
        //подключает клиента в указанную комнату
        public void ConnectToRoom(ClientObject clientObject, RoomObject roomObject)
        {
            roomObject.AddConnection(clientObject);
            RemoveConnection(clientObject);
        }

        //перемещает клиента во временные пользователи и удаляет с комнаты
        public void LeaveRoom(ClientObject clientObject)
        {
            AddConnection(clientObject);
            clientObject.Room.RemoveConnection(clientObject);
            clientObject.Room = null;
        }

        //отключение сервера
        public void Disconnect()
        {
            tcpListener.Stop();
            foreach (var room in rooms)
            {
                foreach (var client in room.Clients)
                {
                    client?.Close();
                }
            }

            foreach (var client in tmpClients)
            {
                client?.Close();
            }
            
            Environment.Exit(0);
        }
    }
}
