using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendAddedMessage : Utils.Objects.Packet
    {
        public Types.game.friend.FriendInformations Friend { get; set; }

        public FriendAddedMessage(Types.game.friend.FriendInformations friend)
            : base(PacketType.FriendAddedMessage)
        {
            Friend = friend;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Friend);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Types.game.friend.FriendInformations friend)
        {
            sender.WriteShort((short)friend.ProtocolID);
            friend.serialize(sender);
        }
    }
}
