using System;
using System.Collections.Generic;

namespace Aldos.Network.Messages.game.friend
{
	public class FriendsListMessage : Utils.Objects.Packet
	{
        public List<Types.game.friend.FriendInformations> Friends { get; set; }

		public FriendsListMessage (List<Types.game.friend.FriendInformations> friends)
			: base(PacketType.FriendsListMessage)
		{
            Friends = friends;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Friends);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, List<Types.game.friend.FriendInformations> friends)
		{
            sender.WriteShort((short)friends.Count);
            foreach (Types.game.friend.FriendInformations friend in friends)
            {
                sender.WriteShort((short)friend.ProtocolID);
                friend.serialize(sender);
            }
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public FriendsListMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

