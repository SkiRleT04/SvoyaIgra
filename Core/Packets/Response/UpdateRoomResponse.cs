using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Response
{
    public class UpdateRoomResponse : Response
    {
        public override RequestType Request { get; set; } = RequestType.UpdateRoom;
        public UpdateRoomType Type { get; set; }
        public Player Player { get; set; }
        public Player Selector { get; set; }
        public Player Respondent { set; get; }
    }
}
