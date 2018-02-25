using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public class CheckAnswerResponse : Response
    {
        public ResponseStatus Status { get; set; }
        public override RequestType Request { get; set; } = RequestType.CheckAnswer;
    }
}
