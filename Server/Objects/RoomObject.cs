using Core.Objects;
using Core.Packets.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects
{
    class RoomObject
    {
        List<ClientObject> clients = new List<ClientObject>();
        public ReadOnlyCollection<ClientObject> Clients { get { return clients.AsReadOnly(); } }
        public Room Info { get; private set; }
        public GameObject Game { get; private set; }
        private ClientObject selector;
        public ClientObject Selector
        {
            get
            {
                if (selector == null && clients.Count == 1)
                    Selector = clients.First();
                return selector;
            }
            set
            {
                Game.StartSelectQuestionTimer();
                selector = value;
            }
        }
        public ServerObject Server { get; set; }
        public ClientObject Respondent { get; set; }

        public List<ClientObject> Respondents { get; set; } = new List<ClientObject>();


        //инициализарует новую комнату
        public RoomObject(int id, string name, int size, ServerObject server)
        {
            Info = new Room(id, name, size);
            Game = new GameObject();
            Game.Room = this;
            Game.Room.Server = server;
        }

        //отправка сообщения всем клиентам комнаты
        public void SendMessageToAllClients(string message)
        {
            foreach (var client in clients)
            {
                if (client.Player != null)
                    client.Writer.WriteLine(message);
            }
        }

        //отправка сообщениям всем клиентам комнаты кроме себя
        public void SendMessageToAllClientsExceptSendingClient(string message, ClientObject clientObject)
        {
            foreach (var client in clients)
            {
                if (client.Id != clientObject.Id && client.Player != null)
                    client.Writer.WriteLine(message);
            }
        }

        //отправка сообщения определенному клиенту находящемуся в комнате
        public void SendMessageToDefiniteClient(string message, ClientObject clientObject)
        {
            clientObject.Writer.WriteLine(message);
        }

        //добавления клиента в комнату
        public void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
            clientObject.Room = this;
            clientObject.Player.Points = 0;
            Info.PlayersCount++;
        }

        //удаления клиента с комнаты
        public void RemoveConnection(ClientObject clientObject)
        {
            clients.Remove(clientObject);
            Info.PlayersCount--;
        }

        //возвращает всех игроков комнаты
        public IEnumerable<Player> GetAllPlayers()
        {
            return clients.Select(c => c.Player).AsEnumerable();
        }

    }
}
