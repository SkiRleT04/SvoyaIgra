using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Client.Objects.Commands
{
    class UpdatePointsCommand : BaseCommand
    {
        public override RequestType RequestType => throw new NotImplementedException();

        public override int Frequency => throw new NotImplementedException();

        public override void Execute(string packet)
        {
            throw new NotImplementedException();
        }
    }
}
