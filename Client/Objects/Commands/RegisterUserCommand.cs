﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Core.Enums;
using Core.Packets.Response;

namespace Client.Objects.Commands
{
    class RegisterUserCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.RegisterUser;

        public override int Frequency => throw new NotImplementedException();

        public override void Execute(BaseResponse baseResponse, Page page)
        {
            throw new NotImplementedException();
        }
    }
}