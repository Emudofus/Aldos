using System;
namespace Aldos.Network.Messages.game.inventory
{
	public class InventoryWeightMessage : Utils.Objects.Packet
	{
		/// <summary>
		/// TODO
		/// </summary>
		public InventoryWeightMessage ()
			: base(PacketType.InventoryWeightMessage)
		{
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteInt(1); // actual pods
			sender.WriteInt(1000); // max pods
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static InventoryWeightMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

