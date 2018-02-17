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
    class RoomLeaveCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            //удаляем клиента с комнаты
            var response = new RoomLeaveResponse();

            //проверяем если клиент подключен к серверу то удаляем с комнаты, а если соединение прервано то пропускаем удаление
            if (client.IsConnected())
            {
                server.LeaveRoom(client);
                Console.WriteLine($"The room ({room.Info.Name}) was left by the user ({client.Player.Login})");
            }
            //отправка всем временным пользователям о обновлении комнаты
            response.Rooms = server.GetFreeRooms();
            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToAllAuthClients(packetResponse);
            //-------------------------------------------------------------------//
            NotifyPlayersAboutUpdateRooms(room);
        }

        //отправка игрокам комнаты информацию об обновлении комнаты
        public void NotifyPlayersAboutUpdateRooms(RoomObject room)
        {
            var responeForPlayers = new GetRoomInfoResponse();
            responeForPlayers.Players = room.GetAllPlayers();
            string packetResponseForPlayers = JsonConvert.SerializeObject(responeForPlayers);
            room.SendMessageToAllClients(packetResponseForPlayers);
        }
    }
}
