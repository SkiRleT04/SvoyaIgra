using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Request;
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
            Console.WriteLine("RegisterUserComand");
            server.SendMessageToDefiniteClient("Ok", client.Id);
        }
    }
}
