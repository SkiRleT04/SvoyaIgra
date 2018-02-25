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
    class SetRespondentCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.SetRespondent;

        public override int Frequency => 5;

        public override void Execute(string packet)
        {
            SetRespondentResponse setRespondentResponse = JsonConvert.DeserializeObject<SetRespondentResponse>(packet);
            GameViewModel gvm = ClientObject.view as GameViewModel;

            switch (setRespondentResponse.Status)
            {
                case ResponseStatus.Ok:
                    gvm.SetRespondent(setRespondentResponse.Player);

                    break;

                case ResponseStatus.Bad:
                   // gvm.BlockAnswerButton(false);
                    break;
            }
                

        }
    }
}
