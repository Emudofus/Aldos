using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Enums
{
    public enum CharacterCreationResult
    {
        OK = 0,
        NO_REASON = 1,
        INVALID_NAME = 2,
        NAME_ALREADY_EXISTS = 3,
        TOO_MANY_CHARACTERS = 4,
        NOT_ALLOWED = 5,
        NEW_PLAYER_NOT_ALLOWED = 6,
        RESTRICED_ZONE = 7,
    }
}
