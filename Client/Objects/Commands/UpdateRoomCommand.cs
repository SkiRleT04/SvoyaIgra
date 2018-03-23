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
    class UpdateRoomCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.UpdateRoom;

        public override int Frequency => 3;

        public override void Execute(string packet)
        {
            UpdateRoomResponse updateRoomResponse = JsonConvert.DeserializeObject<UpdateRoomResponse>(packet);
            GameViewModel gvm = (ClientObject.view as GameViewModel);

            switch (updateRoomResponse.Type)
            {
                case UpdateRoomType.UpdatePlayers:
                    bool isSelector = ClientObject.user.Login == updateRoomResponse.Selector.Login;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        (((((MainWindow)Application.Current.MainWindow).Frame.Content as Game).GameFrame.Content) as CategoriesAndQuestionsTable)?.ChangeButtonEProp(isSelector);
                    });
                    gvm.UpdatePoints(updateRoomResponse.Player);
                    break;

                case UpdateRoomType.UpdateTable:
                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        (((MainWindow)Application.Current.MainWindow).Frame.Content as Game).GameFrame.NavigationService.GoBack();
                    });
                    gvm.BlockAnswerButton(false);
                    break;
            }

        }
    }
}
