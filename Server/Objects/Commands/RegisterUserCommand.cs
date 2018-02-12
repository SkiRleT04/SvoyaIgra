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
    class RegisterUserCommand : BaseCommand
    {
        public override int Frequency => 1;
        public override RequestType Type => RequestType.RegisterUser;


        public override void Excecute(string packet, ClientObject client, ServerObject server)
        {
            Console.WriteLine("Register user");
            var comand = JsonConvert.DeserializeObject<RegisterUserRequest>(packet);
            var response = new RegisterUserResponse();
            response.Status = DB.RegisterUser(comand.User);
            Console.WriteLine($"Register user {response.Status.ToString()}");

            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client.Id);
        }
    }
}
