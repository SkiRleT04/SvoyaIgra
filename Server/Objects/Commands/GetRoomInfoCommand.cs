using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    class GetRoomInfoCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Get room info");
            var response = new GetRoomInfoResponse();
            response.TableQuestions = new System.Collections.ObjectModel.ReadOnlyDictionary<string, IEnumerable<Core.Objects.Question>>(room.Game.TableQuestions);



            foreach (var item in response.TableQuestions)
            {
                foreach (var q in item.Value)
                {
                    Debug.Write((q.Id > 0 ? 1 : 0));
                }
                Debug.WriteLine("");
            }


            response.Players = room.GetAllPlayers();
            response.Selector = room.Selector.Player;

            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToDefiniteClient(packetResponse, client);
            Console.WriteLine("Get room successfully");
        }
    }
}
