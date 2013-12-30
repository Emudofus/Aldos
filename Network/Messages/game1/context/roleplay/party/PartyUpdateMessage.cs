using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyUpdateMessage : Utils.Objects.Packet
    {
        public Global.Character Updated { get; set; }

        public PartyUpdateMessage(Global.Character updated)
            : base(PacketType.PartyUpdateMessage)
        {
            Updated = updated;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Updated);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Character updated)
        {
            new Types.game.context.PartyMemberInformations(updated).serialize(sender);
        }
    }
}
