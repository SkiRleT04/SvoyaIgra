using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Core.Enums;
using Core.Packets.Response;
using Newtonsoft.Json;

namespace Client.Objects.Commands
{
    class UpdatePointsCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.UpdatePoints;

        public override int Frequency => 3;

        public override void Execute(string packet)
        {
            UpdateRoomResponse updatePointsResponse = JsonConvert.DeserializeObject<UpdateRoomResponse>(packet);
            (ClientObject.view as GameViewModel).UpdatePoints(updatePointsResponse.Player);

        }
    }
}
