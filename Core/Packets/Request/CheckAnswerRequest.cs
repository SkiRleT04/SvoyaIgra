using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Request
{
    class CheckAnswerRequest : BaseRequest
    {
        public Question Question { get; set; }
        public Player Player { get; set; }
    }
}
