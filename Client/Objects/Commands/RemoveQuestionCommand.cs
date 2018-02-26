using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.ViewModels;
using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;
using SvoyaIgraClient;
using SvoyaIgraClient.Views;
using SvoyaIgraClient.Views.GameFrames;

namespace Client.Objects.Commands
{
    class RemoveQuestionCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.RemoveQuestion;

        public override int Frequency => 3;

        public override void Execute(string packet)
        {
            RemoveQuestionResponse removeQuestionResponse = JsonConvert.DeserializeObject<RemoveQuestionResponse>(packet);
            GameViewModel gvm = (ClientObject.view as GameViewModel);
            int questionId = removeQuestionResponse.QuestionId;
                gvm.RemoveQuestion(questionId);
                
        }
    }
}
