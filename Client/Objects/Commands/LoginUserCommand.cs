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
using SvoyaIgraClient;
using SvoyaIgraClient.ViewModels;
using SvoyaIgraClient.Views;

namespace Client.Objects.Commands
{
    class LoginUserCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.LoginUser;

        public override int Frequency => 1;


        public void unlock()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (((MainWindow)Application.Current.MainWindow).Frame.Content as Login).btnLogIn.IsEnabled = true;
                (((MainWindow)Application.Current.MainWindow).Frame.Content as Login).btnBack.IsEnabled = true;
            });
        }

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
                    unlock();
                    (ClientObject.view as UserViewModel).Status = "Ошибка на стороне сервера";
                    break;

                case ResponseStatus.UserDoesntExist:
                    unlock();

                    (ClientObject.view as UserViewModel).Status = "Пользователь с таким логином не существует";
                    break;

                case ResponseStatus.WrongPassword:
                    unlock();

                    (ClientObject.view as UserViewModel).Status = "Был введён неверный пароль";
                    break;

                case ResponseStatus.UserIsPlaying:
                    unlock();

                    (ClientObject.view as UserViewModel).Status = "Пользователь с таким логином уже играет";
                    break;

            }
        }
    }
}
