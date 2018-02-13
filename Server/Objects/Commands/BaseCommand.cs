using Core.Enums;
using Core.Packets.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    abstract class BaseCommand
    {
        
        public abstract int Frequency { get; }  //Range(1;5)
        public abstract RequestType Type { get; }
        public abstract void Excecute(string packet, ClientObject client, ServerObject server, RoomObject room);

        public bool TypesAreEqual(RequestType RequestType)
        {
            return Type == RequestType;
        }
    }
}
