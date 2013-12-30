using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendUpdateMessage : Utils.Objects.Packet
    {
        public Types.context.FriendInformations Updated { get; set; }

        public FriendUpdateMessage(Types.context.FriendInformations updated)
            : base(PacketID.FriendUpdateMessage)
        {
            Updated = updated;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Updated);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Types.context.FriendInformations updated)
        {
            sender.WriteShort((short)updated.ProtocolID);
            updated.serialize(sender);
        }
    }
}
