using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyKickRequestMessage : Utils.Objects.Packet
    {
        public int KickedID { get; set; }

        public PartyKickRequestMessage(int kickedId)
            : base(PacketType.PartyKickRequestMessage)
        {
            KickedID = kickedId;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, KickedID);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int kickedId)
        {
            sender.WriteInt(kickedId);
        }

        public static PartyKickRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            return new PartyKickRequestMessage(sender.ReadInt());
        }
    }
}
