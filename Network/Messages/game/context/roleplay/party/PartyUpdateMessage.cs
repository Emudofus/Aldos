using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyUpdateMessage : Utils.Objects.Packet
    {
        public Types.game.context.PartyMemberInformations Updated { get; set; }

        public PartyUpdateMessage(Global.Character updated)
            : base(PacketID.PartyUpdateMessage)
        {
            Updated = new Types.game.context.PartyMemberInformations(updated);
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            Updated.serialize(this);
            return base.Pack();
        }
    }
}
