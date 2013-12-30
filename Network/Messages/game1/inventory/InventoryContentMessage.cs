using System;

namespace Aldos.Network.Messages.game.inventory
{
	public class InventoryContentMessage : Utils.Objects.Packet
	{
		public Global.Character Character {
			get;
			set;
		}
		
		public InventoryContentMessage (Global.Character perso)
			: base(PacketType.InventoryContentMessage)
		{
			Character = perso;
		}
		
		public override Utils.Objects.ByteBuffer Pack()
		{
			serialize(this, Character);
			return base.Pack();
		}
		
		public static void serialize(Utils.Objects.Packet sender, Global.Character perso)
		{
			sender.WriteShort(0); // nItems
            sender.WriteInt(perso.Characteristics.kamas);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static InventoryContentMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

