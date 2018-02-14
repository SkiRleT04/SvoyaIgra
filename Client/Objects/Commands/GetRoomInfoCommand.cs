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
    class GetRoomInfoCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.GetRoomInfo;

        public override int Frequency => 2;

        public override void Execute(string packet)
        {
            //throw new NotImplementedException();
        }
    }
}
