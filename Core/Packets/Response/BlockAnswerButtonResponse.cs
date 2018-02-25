using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Packets.Response
{
    public class BlockAnswerButtonResponse : BaseResponse
    {
        public override RequestType Request => RequestType.BlockAnswerButton;
        public bool IsEnabled { get; set; }
    }
}
