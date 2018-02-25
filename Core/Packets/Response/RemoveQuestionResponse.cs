using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Packets.Response
{
    public class RemoveQuestionResponse : BaseResponse
    {
        public override RequestType Request { get; set; } = RequestType.RemoveQuestion;
        public int QuestionId { get; set; }
    }
}
