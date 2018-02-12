using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Core.Enums;
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
                        SetPage(new Game());
                    });
                    break;

                case ResponseStatus.Bad:
                    (ClientObject.view as UserViewModel).Status = "Неверный логин или пароль";
                    break;
            }
        }
    }
}
