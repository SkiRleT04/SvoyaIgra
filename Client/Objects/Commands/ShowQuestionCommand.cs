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
    class ShowQuestionCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.ShowQuestion;

        public override int Frequency => 4;

        public override void Execute(string packet)
        {
            ShowQuestionResponse showQuestionResponse = JsonConvert.DeserializeObject<ShowQuestionResponse>(packet);
            GameViewModel gvm = ClientObject.view as GameViewModel;
            int questionId = showQuestionResponse.QuestionId;
            gvm.SelectedQuestionId = questionId;
           Application.Current.Dispatcher.Invoke(() =>
            {
                (((((MainWindow)Application.Current.MainWindow).Frame.Content as Game).GameFrame.Content) as CategoriesAndQuestionsTable).HideButton(questionId);
            });
            gvm.ShowQuestion(questionId);
            gvm.RemoveQuestion(questionId);
           

        }
    }
}
