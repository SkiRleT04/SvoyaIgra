using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Client.ViewModels;
using Client.Views;
using Core.Enums;
using Core.Objects;
using Core.Packets.Response;
using Newtonsoft.Json;
using SvoyaIgraClient.ViewModels;
using SvoyaIgraClient.Views;

namespace Client.Objects.Commands
{
    class LoginUserCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.LoginUser;

        public override int Frequency => 1;

        public override void Execute(string packet)
        {
            LoginUserResponse loginUserResponse = JsonConvert.DeserializeObject<LoginUserResponse>(packet);

            switch (loginUserResponse.Status)
            {
                case ResponseStatus.Ok:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SetPage(new RoomsPage());
                        (ClientObject.view as RoomViewModel).Rooms = new ObservableCollection<Room>(loginUserResponse.Rooms);
                    });
                    break;

                case ResponseStatus.Bad:
                    (ClientObject.view as UserViewModel).Status = "Ошибка на стороне сервера";
                    break;

                case ResponseStatus.UserDoesntExist:
                    (ClientObject.view as UserViewModel).Status = "Пользователя с таким логином не существует";
                    break;

                case ResponseStatus.WrongPassword:
                    (ClientObject.view as UserViewModel).Status = "Был введён неверный пароль";
                    break;

                case ResponseStatus.UserIsPlaying:
                    (ClientObject.view as UserViewModel).Status = "Пользователь с таким логином уже играет";
                    break;

            }
        }
    }
}
