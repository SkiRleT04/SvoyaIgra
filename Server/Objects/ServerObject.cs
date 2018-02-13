using Core.Objects;
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
        List<RoomObject> rooms = new List<RoomObject>();
        public ReadOnlyCollection<BaseCommand> Comands => comands.AsReadOnly();
        public ReadOnlyCollection<RoomObject> Rooms => rooms.AsReadOnly();
        List<ClientObject> tmpClients = new List<ClientObject>();



        public ServerObject()
        {
            //initialize comands
            comands.Add(new RegisterUserCommand());
            comands.Add(new LoginUserCommand());
            comands.Add(new GetRoomsCommand());

            comands = comands.OrderByDescending(x => x.Frequency).ToList();
            //initialize rooms
            rooms.Add(new RoomObject(1, "GameRoom1", 5));
            //rooms.Add(new RoomObject(2, "GameRoom2", 3));
        }

        //обновление комнат
        private void UpdateRooms()
        {
            //если пустых комнат больше чем одна, оставляем только одну
            var emptyRooms = rooms.Where(r => r.Info.PlayersCount == 0).ToArray();
            if (emptyRooms.Length > 1)
            {
                for (int i = 0; i < emptyRooms.Length-1; i++)
                    rooms.Remove(emptyRooms[i]);
                return;
            }

            //если в комнате нету свободных мест, добавляем новую
            var freeRooms = rooms.Where(r => r.Info.PlayersCount < r.Info.Size).ToArray();
            if (freeRooms.Length == 0)
            {
                int idRoom = rooms.OrderByDescending(r => r.Info.Id).First().Info.Id + 1;
                rooms.Add(new RoomObject(idRoom, $"GameRoom{idRoom}", 5));
            }
                
        }

        public RoomObject GetRoomById(int id)
        {
            return rooms.FirstOrDefault(r => r.Info.Id == id);
        }


        public List<Room> GetFreeRooms()
        {
            UpdateRooms();
            return rooms.Where(x => x.Info.PlayersCount < x.Info.Size).Select(x => x.Info).ToList();
        }

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

        public void SendMessageToDefiniteClient(string message, string idDefiniteClient)
        {
            foreach (var client in tmpClients)
            {
                if (client.Id == idDefiniteClient)
                {
                    client.Writer.WriteLine(message);
                    break;
                }
            }
        }


        public void AddConnection(ClientObject clientObject)
        {
            tmpClients.Add(clientObject);
        }

        public void RemoveConnection(ClientObject clientObject)
        {
            tmpClients.Remove(clientObject);
        }


        public void ConnectToRoom(ClientObject clientObject, RoomObject roomObject)
        {
            roomObject.AddConnection(clientObject);
            RemoveConnection(clientObject);
        }

        public void LeaveRoom(ClientObject clientObject)
        {
            AddConnection(clientObject);
            clientObject.Room.RemoveConnection(clientObject);
        }

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
