using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Db;

namespace Server.Objects.Commands
{
    class LoginUserCommand : BaseCommand
    {
        public override int Frequency => 3;

        public override RequestType Type => RequestType.LoginUser;

        public override void Excecute(string packet, ClientObject client, ServerObject server, RoomObject room)
        {
            Console.WriteLine("Login user");
            var request = JsonConvert.DeserializeObject<LoginUserRequest>(packet);


            var response = new LoginUserResponse();
            response.Status = DB.AuthUser(request.User);
            Console.WriteLine($"Login user status: {response.Status.ToString()}");
            if (response.Status == ResponseStatus.Ok)
            {
                client.Player = new Player(request.User.Login);
                Console.WriteLine($"User: {request.User.Login} successfully authorized");
            }

            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client.Id);
        }
    }
}
