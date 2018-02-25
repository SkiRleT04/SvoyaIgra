using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Packets.Request
{
    public class UpdateRoomRequest : Request
    {
        public override RequestType Type { get; set; } = RequestType.UpdateRoom;
    }
}
