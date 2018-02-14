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
using SvoyaIgraClient.Views;

namespace Client.Objects.Commands
{
    class RoomJoinCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.RoomJoin;

        public override int Frequency => 3;

        public override void Execute(string packet)
        {
            RoomJoinResponse roomJoinResponse = JsonConvert.DeserializeObject<RoomJoinResponse>(packet);

            switch (roomJoinResponse.Status)
            {
                case ResponseStatus.Ok:

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SetPage(new Game());
                    });
                    break;

                case ResponseStatus.RoomIsFull:
                    //(ClientObject.view as RoomViewModel).Status = "Комната полна";
                    MessageBox.Show("Комната полна");
                    break;

                case ResponseStatus.Bad:
                    //(ClientObject.view as RoomViewModel).Status = "Ошибка";
                    MessageBox.Show("Ошибка на стороне сервера");
                    break;

            }
        }
    }
}
