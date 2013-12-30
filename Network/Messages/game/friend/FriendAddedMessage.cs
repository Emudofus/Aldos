using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendAddedMessage : Utils.Objects.Packet
    {
        public Types.context.FriendInformations Friend { get; set; }

        public FriendAddedMessage(Types.context.FriendInformations friend)
            : base(PacketID.FriendAddedMessage)
        {
            Friend = friend;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Friend);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Types.context.FriendInformations friend)
        {
            sender.WriteShort((short)friend.ProtocolID);
            friend.serialize(sender);
        }
    }
}
