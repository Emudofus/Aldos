using System;
namespace Aldos.Network.Messages.game.inventory.spells
{
	public class SpellListMessage : Utils.Objects.Packet
	{
		/// <summary>
		/// TODO
		/// </summary>
		public SpellListMessage ()
			: base(PacketType.SpellListMessage)
		{
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteBool(true);
			sender.WriteShort(0);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static SpellListMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

