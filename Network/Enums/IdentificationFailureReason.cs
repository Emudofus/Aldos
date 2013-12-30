using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Enums
{
    public enum IdentificationFailureReason
    {
        Version = 1,
        Login = 2,
        Banned = 3,
        Kicked = 4,
        Maintenance = 5,
        UNKNOWN = 99,
    }
}
