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
using SvoyaIgraClient;
using SvoyaIgraClient.ViewModels;
using SvoyaIgraClient.Views;

namespace Client.Objects.Commands
{
    class RegisterUserCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.RegisterUser;

        public override int Frequency => 1;

        public override void Execute(string packet)
        {
            RegisterUserResponse registerUserResponse = JsonConvert.DeserializeObject<RegisterUserResponse>(packet);

            switch (registerUserResponse.Status )
            {
                case ResponseStatus.Ok:
                    MessageBox.Show("Ok");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SetPage(new Game());
                    });
                    break;

                case ResponseStatus.LoginIsTaken:
                    (ClientObject.view as UserViewModel).Login = "kek";
                    break;
            }



        }
    }
}
