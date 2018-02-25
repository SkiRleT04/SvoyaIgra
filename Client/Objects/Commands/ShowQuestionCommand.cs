using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Client.Objects.Commands
{
    class ShowQuestionCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.ShowQuestion;

        public override int Frequency => 4;

        public override void Execute(string packet)
        {
            ShowQuestionResponse showQuestionResponse = JsonConvert.DeserializeObject<ShowQuestionResponse>(packet);
            GameViewModel gvm = ClientObject.view as GameViewModel;
            gvm.ShowQuestion(showQuestionResponse.QuestionId);
            
        }
    }
}
