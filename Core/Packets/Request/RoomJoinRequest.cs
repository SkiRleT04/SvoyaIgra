using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Request
{
    public class RoomJoinRequest : BaseRequest
    {
        public override RequestType Type => RequestType.RoomJoin;
        public User User { get; set; }
        public Room Room { get; set; }
    }
}
