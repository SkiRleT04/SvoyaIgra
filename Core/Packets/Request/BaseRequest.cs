using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Request
{
    public abstract class BaseRequest
    {
        public  RequestType Type { get; set; }
    }
}
