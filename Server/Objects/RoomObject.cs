using Core.Objects;
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


        public RoomObject(int id, string name, int size)
        {
            Info = new Room(id, name, size);
            Game = new GameObject();
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
                if (client.Id != idSendingUser && client.Player != null)
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

        public void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
            clientObject.Room = this;
        }

        public void RemoveConnection(ClientObject clientObject)
        {
            clients?.Remove(clientObject);
        }

    }
}
