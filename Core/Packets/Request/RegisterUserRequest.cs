using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Request
{
    public class RegisterUserRequest : Request
    {
        public User User { get; set; }
        public override RequestType Type { get; set; } = RequestType.RegisterUser;
    }
}
