using System;
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
        GetRoomInfo,
        CheckAnswer,
        SetRespondent,
        GetWinner,
        RoomJoin,
        RoomLeave,
        GetRooms,
        UpdateRoom,
        ShowQuestion
    }
}
