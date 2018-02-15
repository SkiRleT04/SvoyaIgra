using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum ResponseStatus
    {
        Ok,
        Bad,
        LoginIsTaken,
        WrongPassword,
        UserDoesntExist,
        RoomIsFull,
        UserIsPlaying
    }
}
