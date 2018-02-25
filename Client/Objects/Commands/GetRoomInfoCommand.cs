using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Client.ViewModels;
using Core.Enums;
using Core.Objects;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Client.Objects.Commands
{
    class GetRoomInfoCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.GetRoomInfo;

        public override int Frequency => 4;

        public override void Execute(string packet)
        {
            GetRoomInfoResponse getRoomInfoResponse = JsonConvert.DeserializeObject<GetRoomInfoResponse>(packet);

            /*Application.Current.Dispatcher.Invoke(() =>
            {
                SetPage(new RoomsPage());
            });*/

            (ClientObject.view as GameViewModel).Players = new ObservableCollection<Player>(getRoomInfoResponse.Players);

            if(getRoomInfoResponse.TableQuestions != null)
             (ClientObject.view as GameViewModel).QuestionsTable= getRoomInfoResponse.TableQuestions;

           
        }
    }
}
