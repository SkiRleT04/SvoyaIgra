using Core.Objects;
using Core.Packets.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects
{
    class ClientObject
    {
        TcpClient client;
        ServerObject server;
        public RoomObject Room { get; set; }
        public Player Player { get; set; }
        public string Id { get; set; }
        public StreamReader Reader { get; private set; }
        public StreamWriter Writer { get; private set; }

        public ClientObject(TcpClient client, ServerObject server)
        {
            Player = null;
            Id = Guid.NewGuid().ToString();
            this.client = client;
            this.server = server;
            server.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
                Writer.AutoFlush = true;
                Console.WriteLine($"{Id}: вошел в чат");
                while (true)
                {
                    try
                    {
                        string packet = Reader.ReadLine();
                        if (!String.IsNullOrEmpty(packet))
                        {
                            Console.WriteLine(packet);
                            var typePacket = JsonConvert.DeserializeObject<BaseRequest>(packet).Type;
                            foreach (var comand in server.Comands)
                            {
                                if (comand.TypesAreEqual(typePacket))
                                {
                                    comand.Excecute(packet, this, server, Room);
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"{Id}: покинул чат");
                        //server.SendMessageToAllClientsExceptSendingClient($"{Player.Login}: покинул чат", Id);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (Room==null)
                    server.RemoveConnection(this);
                else
                    Room.RemoveConnection(this);
                Close();
            }
        }

        public void Close()
        {
            Reader?.Close();
            Writer?.Close();
            client?.Close();
        }
    }
}
