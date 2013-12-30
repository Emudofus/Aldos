using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyJoinMessage : Utils.Objects.Packet
    {
        public Global.Party Party { get; set; }

        public PartyJoinMessage(Global.Party party)
            : base(PacketID.PartyJoinMessage)
        {
            Party = party;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            Party.serializeAs_PartyJoinMessage(this);
            return base.Pack();
        }
    }
}
