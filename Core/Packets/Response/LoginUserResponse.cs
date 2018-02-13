using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public class LoginUserResponse : Response
    {
        public ResponseStatus Status { get; set; }
        public override RequestType Request { get; set; } = RequestType.LoginUser;
        public IEnumerable<Room> Rooms { get; set; }

    }
}
