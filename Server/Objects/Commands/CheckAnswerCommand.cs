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


        }
    }
}
