using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Client.ViewModels;
using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Client.Objects.Commands
{
    class CheckAnswerCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.CheckAnswer;

        public override int Frequency => 4;

        public override void Execute(string packet)
        {
            CheckAnswerResponse checkAnswerResponse = JsonConvert.DeserializeObject<CheckAnswerResponse>(packet);
            (ClientObject.view as GameViewModel).UpdatePoints(checkAnswerResponse.Player);
            switch (checkAnswerResponse.Status)
            {
                case ResponseStatus.Ok:
                    //(ClientObject.view as GameViewModel).UpdatePoints(checkAnswerResponse.Player);
                    //(ClientObject.view as GameViewModel).Status = "Был введён неверный пароль";
                    break;

                case ResponseStatus.Bad:
                   // (ClientObject.view as GameViewModel).Status = "Пользователь с таким логином уже играет";
                    break;
            }
        }
    }
}
