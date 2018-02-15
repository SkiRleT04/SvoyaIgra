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
        static object locker = new Object();
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            //lock (locker)
            //{
                Console.WriteLine("Login user");
                var request = JsonConvert.DeserializeObject<LoginUserRequest>(packet);
                var response = new LoginUserResponse();
                response.Status = DB.AuthUser(request.User);

                //проверяем играет ли пользователь
                if (UserIsPlaying(request.User, server))
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
            //}
        }


        //проверяет играет сейчас пользователь или нет
        private bool UserIsPlaying(User user, ServerObject server)
        {
            if (server.TmpClients.Count(u => u.Player != null && u.Player.Login == user.Login) > 0)
                return true;

            foreach (var room in server.Rooms)
            {
                if (room.Clients.Count(u => u.Player != null && u.Player.Login == user.Login) > 0)
                    return true;
            }
            return false;
        }
    }
}
