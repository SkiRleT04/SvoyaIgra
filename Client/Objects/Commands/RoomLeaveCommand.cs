using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.ViewModels;
using Client.Views;
using Core.Enums;
using Core.Objects;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Client.Objects.Commands
{
    class RoomLeaveCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.RoomLeave;

        public override int Frequency => 2;

        public override void Execute(string packet)
        {
            RoomLeaveResponse roomLeaveResponse = JsonConvert.DeserializeObject<RoomLeaveResponse>(packet);

            Application.Current.Dispatcher.Invoke(() =>
            {
                SetPage(new RoomsPage());
            });

            (ClientObject.view as RoomViewModel).Rooms = new ObservableCollection<Room>(roomLeaveResponse.Rooms);    
            

        }
    }
}
