using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyMemberRemoveMessage : Utils.Objects.Packet
    {
        public int LeavingPlayerID { get; set; }

        public PartyMemberRemoveMessage(Global.Character leavingPlayer)
            : base(PacketID.PartyMemberRemoveMessage)
        {
            LeavingPlayerID = leavingPlayer.Id;
        }

        public PartyMemberRemoveMessage(int leavingPlayerId)
            : base(PacketID.PartyMemberRemoveMessage)
        {
            LeavingPlayerID = leavingPlayerId;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, LeavingPlayerID);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int leavingPlayerId)
        {
            sender.WriteInt(leavingPlayerId);
        }
    }
}
