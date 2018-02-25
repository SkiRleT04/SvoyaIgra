using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Packets.Response
{
    public class GetRoomInfoResponse : Response
    {
        public override RequestType Request { get; set; } = RequestType.GetRoomInfo;

        public ReadOnlyDictionary<string, IEnumerable<Question>> TableQuestions { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public Player Selector { get; set; }

    }
}
