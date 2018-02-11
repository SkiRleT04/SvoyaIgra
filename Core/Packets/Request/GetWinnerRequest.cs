using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Request
{
    public class GetWinnerRequest : Request
    {
        public override RequestType Type { get; set; } = RequestType.GetWinner;
    }
}
