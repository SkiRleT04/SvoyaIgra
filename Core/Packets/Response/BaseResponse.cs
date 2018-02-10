using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public abstract class BaseResponse
    {
        
        public RequestType Request { get; }
    }
}
