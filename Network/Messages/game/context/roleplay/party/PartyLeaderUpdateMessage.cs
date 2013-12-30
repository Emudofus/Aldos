using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyLeaderUpdateMessage : Utils.Objects.Packet
    {
        public int NewLeaderID { get; set; }

        public PartyLeaderUpdateMessage(Global.Character newLeader)
            : base(PacketID.PartyLeaderUpdateMessage)
        {
            NewLeaderID = newLeader.Id;
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
    }
}
