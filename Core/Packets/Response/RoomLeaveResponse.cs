using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Response
{
    public class RoomLeaveResponse : BaseResponse
    {
        public override RequestType Request => RequestType.RoomLeave;
        public IEnumerable<Room> Rooms { get; set; }
        //add field user or player
    }
}
