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
    abstract class BaseCommand
    {
        public abstract RequestType RequestType {get;}
        public abstract int Frequency { get; }
        public abstract void Execute(BaseResponse baseResponse, Page page);

        public bool TypesAreEqual(RequestType reqestType)
        {
            return RequestType.Equals(reqestType);
        }

    }
}
