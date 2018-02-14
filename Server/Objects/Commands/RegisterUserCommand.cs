using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Db;

namespace Server.Objects.Commands
{
    class RegisterUserCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Register user");
            var request = JsonConvert.DeserializeObject<RegisterUserRequest>(packet);


            var response = new RegisterUserResponse();
            response.Status = DB.RegisterUser(request.User);
            Console.WriteLine($"Register user status: {response.Status.ToString()}");

            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client);
        }
    }
}
