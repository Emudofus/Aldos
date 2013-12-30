using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.basic
{
	public class BasicTimeMessage : Utils.Objects.Packet
	{
		public BasicTimeMessage ()
		{
			base.ID = PacketType.BasicTimeMessage;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteInt(Global.Environnement.getInstance().Timestamp); // timestamp
			sender.WriteShort(3600); // timezoneOffset
		}
		public static BasicTimeMessage deserialize(Utils.Objects.Packet sender)
		{
			sender.ReadInt(); // timestamp
			sender.ReadShort(); // timezoneOffset
			return new BasicTimeMessage();
		}
	}
}

