using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Packets.Response
{
    public class RoomJoinResponse : BaseResponse
    {
        public override RequestType Request => RequestType.RoomJoin;
        public ResponseStatus Status { get; set; }
    }
}
