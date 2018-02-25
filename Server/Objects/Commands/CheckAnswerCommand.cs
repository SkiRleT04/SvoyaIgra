using Core.Enums;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    class CheckAnswerCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Check answer");
            var request = JsonConvert.DeserializeObject<CheckAnswerRequest>(packet);
            var response = new CheckAnswerResponse();

            response.Status = DB.CheckAnswer(request.Question);
            Console.WriteLine($"Answer status: {response.Status }");
            int points = request.Question.Points * -1;
            if (response.Status == ResponseStatus.Ok)
            {
                points = request.Question.Points;
                room.Selector = client;
            }
            client.UpdatePoints(points);
            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToDefiniteClient(packetResponse, client);
            //отправляем уведомление об обновлении счета игрока
            NotifyPlayersAboutUpdateRoom(client, room);
        }

        private void NotifyPlayersAboutUpdateRoom(ClientObject client, RoomObject room)
        {
            var response = new UpdateRoomResponse();
            response.Player = client.Player;
            response.Selector = room.Selector.Player;
            response.Respondent = room.Respondent.Player;
            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToAllClients(packetResponse);
        }
    }
}
