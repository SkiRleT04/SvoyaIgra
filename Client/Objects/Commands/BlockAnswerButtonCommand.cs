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
    class BlockAnswerButtonCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.BlockAnswerButton;

        public override int Frequency => 5;

        public override void Execute(string packet)
        {
            BlockAnswerButtonResponse blockAnswerButtonResponse = JsonConvert.DeserializeObject<BlockAnswerButtonResponse>(packet);
            GameViewModel gvm = (ClientObject.view as GameViewModel);
            gvm.BlockAnswerButton(blockAnswerButtonResponse.IsEnabled);
        }
    }
}
