using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Packets.Request;

namespace Server.Objects.Commands
{
    class RegisterUserComand : BaseCommand
    {
        public override int Frequency => 1;

        public override RequestType Type => RequestType.RegisterUser;

        public override void Excecute(BaseRequest packet, ClientObject client, ServerObject server)
        {
            throw new NotImplementedException();
        }
    }
}
