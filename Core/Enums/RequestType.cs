﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum RequestType
    {
        RegisterUser,
        LoginUser,
        GetQuestionsTable,
        CheckAnswer,
        SetRespondent,
        GetWinner,
        RoomJoin,
        RoomLeave,
        GetRooms
    }
}
