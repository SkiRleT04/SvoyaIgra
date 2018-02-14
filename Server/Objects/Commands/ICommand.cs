using Core.Enums;
using Core.Packets.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    interface ICommand
    {
        void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "");
    }
}
