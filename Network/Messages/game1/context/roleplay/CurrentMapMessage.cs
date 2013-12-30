using System;
namespace Aldos.Network.Messages.game.context.roleplay
{
	public class CurrentMapMessage : Utils.Objects.Packet
	{
		public int MapID {
			get;
			set;
		}
		
		public CurrentMapMessage (int mapid)
			: base(PacketType.CurrentMapMessage)
		{
			MapID = mapid;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, MapID);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, int mapid)
		{
			sender.WriteInt(mapid);
		}
		
		public static CurrentMapMessage deserialize(Utils.Objects.Packet sender)
		{
			return new CurrentMapMessage(sender.ReadInt());
		}
	}
}

