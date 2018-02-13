using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Server.Objects.Commands
{
    class GetRoomsCommand : BaseCommand
    {
        public override int Frequency => 3;

        public override RequestType Type => RequestType.GetRooms;

        public override void Excecute(string packet, ClientObject client, ServerObject server, RoomObject room)
        {
            Console.WriteLine("Get rooms");
            var response = new GetRoomResponse(server.GetFreeRooms());
            
            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client.Id);
        }
    }
}
