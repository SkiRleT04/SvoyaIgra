using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public abstract class Response
    {
        public abstract RequestType Request { get; set; }
    }
}
