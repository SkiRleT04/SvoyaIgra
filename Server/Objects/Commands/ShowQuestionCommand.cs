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
    class ShowQuestionCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Show question");
            var request = JsonConvert.DeserializeObject<ShowQuestionRequest>(packet);
            var response = new ShowQuestionResponse();
            response.QuestionId = request.QuestionId;
            room.Game.CurrentQuestionId = request.QuestionId;
            string packetResponse = JsonConvert.SerializeObject(response);

            //останавливаем таймер когда игрок выбрал вопрос
            room.Game.StopSelectQuestionTimer();
            //запускаем таймер для нажания на кнопку "ответ"
            room.Game.AnswerButtonClickTimer.Start();


            room.SendMessageToAllClients(packetResponse);
        }
    }
}
