using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    class GetRoomInfoCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Get room info");
            var response = new GetRoomInfoResponse();
            //response.TableQuestions = room.Game.TableQuestions;
            response.Players = room.GetAllPlayers();

            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToDefiniteClient(packetResponse, client);
            Console.WriteLine("Get room successfully");
        }
    }
}
