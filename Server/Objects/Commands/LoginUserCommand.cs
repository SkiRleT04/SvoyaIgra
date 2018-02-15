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
    class LoginUserCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Login user");
            var request = JsonConvert.DeserializeObject<LoginUserRequest>(packet);

            var response = new LoginUserResponse();
            response.Status = DB.AuthUser(request.User);

            //проверяем играет ли пользователь
            if (server.UserIsPlaying(client))
                response.Status = ResponseStatus.UserIsPlaying;

            Console.WriteLine($"Login user status: {response.Status.ToString()}");

            //если пользователь с таким логином и паролем существует и не играет
            if (response.Status == ResponseStatus.Ok)
            {
                response.Rooms = server.GetFreeRooms().AsEnumerable();
                client.Player = new Player(request.User.Login);
                Console.WriteLine($"User: {request.User.Login} successfully authorized");
            }

            string packetResponse = JsonConvert.SerializeObject(response);
            server.SendMessageToDefiniteClient(packetResponse, client);
        }
    }
}
