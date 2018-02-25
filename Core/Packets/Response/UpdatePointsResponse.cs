using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Objects;

namespace Core.Packets.Response
{
    class UpdatePointsResponse : Response
    {
        public override RequestType Request { get; set; } = RequestType.UpdatePoints;       
        public Player Player { get; set; }
    }
}
