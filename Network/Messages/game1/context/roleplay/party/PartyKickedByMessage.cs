using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyKickedByMessage : Utils.Objects.Packet
    {
        public int KickerID { get; set; }

        public PartyKickedByMessage(Global.Character kicker)
            : base(PacketType.PartyKickedByMessage)
        {
            KickerID = kicker.Guid;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, KickerID);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int kickerId)
        {
            sender.WriteInt(kickerId);
        }
    }
}
