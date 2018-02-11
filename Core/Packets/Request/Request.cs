using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Request
{
    public abstract class Request
    {
        public abstract RequestType Type { get; set; }
    }
}
