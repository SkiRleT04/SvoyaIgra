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
        //Range(1;5)
        public abstract int Frequency { get; }
        public abstract RequestType Type { get; }
        public abstract void Excecute(string packet, ClientObject client, ServerObject server);

        public bool TypesAreEqual(RequestType RequestType)
        {
            return Type == RequestType;
        }
    }
}
