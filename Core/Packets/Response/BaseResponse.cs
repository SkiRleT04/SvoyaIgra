﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public class BaseResponse : Response
    {
        public override RequestType Request { get; set; }
    }
}
