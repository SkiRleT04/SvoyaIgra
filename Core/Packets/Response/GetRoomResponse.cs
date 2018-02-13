using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public class GetRoomResponse:BaseResponse
    {
        public override RequestType Request => RequestType.GetRooms;
        public IEnumerable<Room> Rooms { get; set; }

        public GetRoomResponse(IEnumerable<Room> rooms)
        {
            Rooms = rooms;
        }

        public GetRoomResponse()
        {
            
        }
    }
}
