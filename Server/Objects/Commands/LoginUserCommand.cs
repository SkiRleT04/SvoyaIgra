using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Server.Objects.Commands
{
    class LoginUserCommand : BaseCommand
    {
        public override int Frequency => 3;

        public override RequestType Type => RequestType.LoginUser;

        public override void Excecute(string packet, ClientObject client, ServerObject server)
        {
            Console.WriteLine("Login user");
            var comand = JsonConvert.DeserializeObject<LoginUserRequest>(packet);
            var response = new LoginUserResponse();

            using (var db = new ApplicationContext())
            {
                if (db.Users.FirstOrDefault(x => x.Login == comand.User.Login && x.Password == comand.User.Password) == null)
                    response.Status = ResponseStatus.Bad;
                else
                {
                    response.Status = ResponseStatus.Ok;
                }
            }

            Console.WriteLine("Login user done");
            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client.Id);

            
        }
    }
}
