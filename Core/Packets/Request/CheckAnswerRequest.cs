using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Request
{
    public class CheckAnswerRequest : Request
    {
        public Question Question { get; set; }
        public Player Player { get; set; }
        public override RequestType Type { get; set; } = RequestType.CheckAnswer;
    }
}
