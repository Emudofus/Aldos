using System;
namespace Aldos.Network.Messages.game.friend
{
	public class IgnoredListMessage : Utils.Objects.Packet
	{
		/// <summary>
		/// TODO
		/// </summary>
		public IgnoredListMessage ()
			: base(PacketID.IgnoredListMessage)
		{
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteShort(0);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static IgnoredListMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

