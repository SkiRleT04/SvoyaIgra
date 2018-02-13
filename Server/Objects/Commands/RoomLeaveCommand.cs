using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Server.Objects.Commands
{
    class RoomLeaveCommand : BaseCommand
    {
        public override int Frequency => 3;

        public override RequestType Type => RequestType.RoomLeave;

        public override void Excecute(string packet, ClientObject client, ServerObject server, RoomObject room)
        {
            var request = JsonConvert.DeserializeObject<RoomLeaveRequest>(packet);
            var response = new RoomLeaveResponse();
            if (room != null)
            {
                server.LeaveRoom(client);
                Console.WriteLine($"Leave to room {room.Info.Name}");
                //send data game
            }
            
        }
    }
}
