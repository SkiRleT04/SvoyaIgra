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
    class RegisterUserComand : BaseCommand
    {
        public override int Frequency => 1;
        public override RequestType Type => RequestType.RegisterUser;


        public override void Excecute(string packet, ClientObject client, ServerObject server)
        {
            var comand = JsonConvert.DeserializeObject<RegisterUserRequest>(packet);
            var response = new RegisterUserResponse();
            using (var db = new ApplicationContext())
            {
                if (db.Users.FirstOrDefault(x => x.Login == comand.User.Login) != null)
                    response.Status = ResponseStatus.LoginIsTaken;
                else
                {
                    db.Users.Add(comand.User);
                    db.SaveChanges();
                    response.Status = ResponseStatus.Ok;
                }
            }
            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client.Id);
        }
    }
}
