using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Core.Enums;
using Core.Packets.Response;

namespace Client.Objects.Commands
{
    class LoginUserCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.LoginUser;

        public override int Frequency => 1;

        public override void Execute(string response, Page page)
        {
            throw new NotImplementedException();
        }
    }
}
