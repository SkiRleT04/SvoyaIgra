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
            int points = (response.Status == ResponseStatus.Ok) ? request.Question.Points : request.Question.Points * -1;
            client.UpdatePoints(points);
            response.Player = client.Player;
            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client);
            //отправляем уведомление об обновлении счета игрока
            NotifyPlayersAboutUpdatePoints(client, room);
        }

        private void NotifyPlayersAboutUpdatePoints(ClientObject client, RoomObject room)
        {
            var response = new UpdatePointsResponse();
            response.Player = client.Player;
            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToAllClientsExceptSendingClient(packetResponse, client);
        }
    }
}
