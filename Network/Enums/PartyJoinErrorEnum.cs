using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Enums
{
    public enum PartyJoinErrorEnum
    {
        UNKNOWN = 0,
        PLAYER_NOT_FOUND = 1,
        PARTY_NOT_FOUND = 2,
        PARTY_FULL = 3,
        PLAYER_BUSY = 4,
        PLAYER_IS_ALREADY_BEING_INVITED = 5,
        PLAYER_ALREADY_IN_A_PARTY = 6,
    }
}
