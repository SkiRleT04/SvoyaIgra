﻿using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    class SetRespondentCommand : ICommand
    {
        static object locker = new Object();
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            lock (locker)
            {
                var response = new SetRespondentResponse();
                if (room.Respondent == null)
                {
                    room.Respondent = client;
                    response.Status = ResponseStatus.Ok;
                    response.Player = client.Player;
                    string packetResponse = JsonConvert.SerializeObject(response);
                    room.SendMessageToDefiniteClient(packetResponse, client);

                }
                else
                {
                    response.Status = ResponseStatus.Bad;
                    response.Player = room.Respondent.Player;
                    string packetResponse = JsonConvert.SerializeObject(response);
                    room.SendMessageToAllClientsExceptSendingClient(packetResponse, client);
                }
            }
        }
    }
}
