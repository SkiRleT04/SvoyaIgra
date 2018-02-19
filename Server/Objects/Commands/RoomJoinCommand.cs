using System;
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
            //если запрашиваемае комната существует
            if (roomObject != null)
            {
                //если нету свободных мест в комнате
                if (roomObject.Info.PlayersCount >= roomObject.Info.Size)
                    response.Status = ResponseStatus.RoomIsFull;
                else
                {
                    //конектим пользователя к комнате
                    server.ConnectToRoom(client, roomObject);
                    response.Status = ResponseStatus.Ok;
                    Console.WriteLine($"User {client.Player.Login} connect to room {roomObject.Info.Name}");
                }
            }
            else
                response.Status = ResponseStatus.Bad;
            Console.WriteLine($"Join to room status: {response.Status.ToString()}");
            string packetResponse = JsonConvert.SerializeObject(response);
            //отправляем пользователю статус подключения к комнате
            roomObject.SendMessageToDefiniteClient(packetResponse, client);
            //----------------------------------------------------------------------------//
            NotifyTempUsersAboutUpdateRooms(server);
            //---------------------Отправить игрокам комнаты информацию об новом игроке----------------------//
            NotifyPlayersAboutUpdateRooms(roomObject);
            //---------------------Отправить игроку комнаты информации о игре----------------------//
            server.Commands[RequestType.GetRoomInfo]?.Excecute(client, server, roomObject);
        }

        //отправка всем временным пользователям о обновлении комнат
        private void NotifyTempUsersAboutUpdateRooms(ServerObject server)
        {
            var responseForTempClient = new RoomLeaveResponse();
            responseForTempClient.Rooms = server.GetFreeRooms();
            string packetResponseForTempClient = JsonConvert.SerializeObject(responseForTempClient);
            server.SendMessageToAllAuthClients(packetResponseForTempClient);
        }

        //отправка игрокам комнаты информацию об обновлении комнаты
        private void NotifyPlayersAboutUpdateRooms(RoomObject room)
        {
            var responeForPlayers = new GetRoomInfoResponse();
            responeForPlayers.Players = room.GetAllPlayers();
            string packetResponseForPlayers = JsonConvert.SerializeObject(responeForPlayers);
            room.SendMessageToAllClients(packetResponseForPlayers);
        }
    }
}
