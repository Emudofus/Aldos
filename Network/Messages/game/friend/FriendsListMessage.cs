using System;
using System.Collections.Generic;

namespace Aldos.Network.Messages.game.friend
{
	public class FriendsListMessage : Utils.Objects.Packet
	{
        public Global.FriendList Friends { get; set; }

        public FriendsListMessage(Global.FriendList friends)
			: base(PacketID.FriendsListMessage)
		{
            Friends = friends;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Friends);
			return base.Pack ();
		}

        public static void serialize(Utils.Objects.Packet sender, Global.FriendList friends)
		{
            sender.WriteShort((short)friends.Count);
            foreach (Types.context.FriendInformations friend in friends.ToList())
            {
                    sender.WriteShort((short)friend.ProtocolID);
                    friend.serialize(sender);
            }
		}
	}
}

