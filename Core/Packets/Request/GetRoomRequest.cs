using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Request
{
    public class GetRoomRequest:BaseRequest
    {
        public override RequestType Type { get; set; } = RequestType.GetRooms;
    }
}
