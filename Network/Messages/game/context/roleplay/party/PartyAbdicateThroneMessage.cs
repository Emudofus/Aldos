using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyAbdicateThroneMessage : Utils.Objects.Packet
    {
        public int NewLeaderID { get; set; }

        public PartyAbdicateThroneMessage(int newLeaderId)
            : base(PacketID.PartyAbdicateThroneMessage)
        {
            NewLeaderID = newLeaderId;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, NewLeaderID);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int newLeaderId)
        {
            sender.WriteInt(newLeaderId);
        }

        public static PartyAbdicateThroneMessage deserialize(Utils.Objects.Packet sender)
        {
            return new PartyAbdicateThroneMessage(sender.ReadInt());
        }
    }
}
