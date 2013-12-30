using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendAddFailureMessage : Utils.Objects.Packet
    {
        public Enums.ListAddFailureEnum Reason { get; set; }

        public FriendAddFailureMessage(Enums.ListAddFailureEnum reason)
            : base(PacketID.FriendAddFailureMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.ListAddFailureEnum reason)
        {
            sender.WriteByte((byte)reason);
        }
    }
}
