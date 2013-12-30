using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendUpdateMessage : Utils.Objects.Packet
    {
        public Types.game.friend.FriendInformations Updated { get; set; }

        public FriendUpdateMessage(Types.game.friend.FriendInformations updated)
            : base(PacketType.FriendUpdateMessage)
        {
            Updated = updated;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Updated);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Types.game.friend.FriendInformations updated)
        {
            sender.WriteShort((short)updated.ProtocolID);
            updated.serialize(sender);
        }
    }
}
