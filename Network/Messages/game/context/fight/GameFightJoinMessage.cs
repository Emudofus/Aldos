using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.fight
{
    class GameFightJoinMessage : Utils.Objects.Packet
    {
        public GameFightJoinMessage()
            : base(PacketID.GameFightJoinMessage)
        {
        }
    }
}
