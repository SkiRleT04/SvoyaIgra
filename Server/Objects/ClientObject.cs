using Core.Enums;
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

        //создвет клиента и помещает во временные подклбчения
        public ClientObject(TcpClient client, ServerObject server)
        {
            Player = null;
            Id = Guid.NewGuid().ToString();
            this.client = client;
            this.server = server;
            server.AddConnection(this);
        }

        //прослушивания каждого клиента и обработка присланных команд
        public void Process()
        {
            try
            {
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
                Writer.AutoFlush = true;
                Console.WriteLine($"{Id}: connected");
                while (true)
                {
                    try
                    {
                        string packet = Reader.ReadLine();
                        if (!String.IsNullOrEmpty(packet))
                        {
                            Console.WriteLine(packet);
                            var typePacket = JsonConvert.DeserializeObject<BaseRequest>(packet).Type;
                            server.Commands[typePacket]?.Excecute(this, server, Room, packet);
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"{Id}: leave");
                        server.Commands[RequestType.RoomLeave]?.Excecute(this, server, Room);
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
                if (IsInTheRoom())
                    Room.RemoveConnection(this);
                else
                    server.RemoveConnection(this);
                Close();
            }
        }

        //проверяет находится ли клиент в комнате
        private bool IsInTheRoom()
        {
            return Room != null;
        }

        //освобождает ресурсы клиента
        public void Close()
        {
            client?.Close();
            Reader?.Close();
            Writer?.Close();
        }
    }
}
