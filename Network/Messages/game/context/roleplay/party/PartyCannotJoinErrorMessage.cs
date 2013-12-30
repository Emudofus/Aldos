using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyCannotJoinErrorMessage : Utils.Objects.Packet
    {
        public Enums.PartyJoinErrorEnum Reason { get; set; }

        public PartyCannotJoinErrorMessage(Enums.PartyJoinErrorEnum reason)
            : base(PacketID.PartyCannotJoinErrorMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.PartyJoinErrorEnum reason)
        {
            sender.WriteByte((byte)reason);
        }
    }
}
