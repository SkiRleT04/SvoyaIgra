using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Core.Enums;
using Core.Packets.Response;

namespace Client.Objects.Commands
{
    class GetQuestionsTableCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.GetQuestionsTable;

        public override int Frequency => 2;

        public override void Execute(string response, Page page)
        {
            throw new NotImplementedException();
        }
    }
}
