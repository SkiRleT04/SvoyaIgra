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
    class RoomJoinCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            var request = JsonConvert.DeserializeObject<RoomJoinRequest>(packet);
            Console.WriteLine($"Join to room {request.Room.Name}");
            var roomObject = server.GetRoomById(request.Room.Id);

            var response = new RoomJoinResponse();

            if (roomObject != null)
            {
                if (roomObject.Info.PlayersCount >= roomObject.Info.Size)
                    response.Status = ResponseStatus.RoomIsFull;
                else
                {
                    server.ConnectToRoom(client, roomObject);
                    response.Status = ResponseStatus.Ok;
                    Console.WriteLine($"User {client.Player.Login} connect to room {roomObject.Info.Name}");
                }

            }
            else
                response.Status = ResponseStatus.Bad;
            Console.WriteLine($"Join to room status: {response.Status.ToString()}");
            string packetResponse = JsonConvert.SerializeObject(response);
            roomObject.SendMessageToDefiniteClient(packetResponse, client);

            //send addition info
           // if (response.Status == ResponseStatus.Ok)
                //server.Commands[RequestType.GetRoomInfo]?.Excecute(client, server, roomObject);
           // if (response.Status == ResponseStatus.RoomIsFull)
                //server.Commands[RequestType.GetRooms]?.Excecute(client, server, roomObject);

        }
    }
}
