using System;
namespace Aldos.Network.Messages.game.context.roleplay
{
	public class MapComplementaryInformationsDataMessage : Utils.Objects.Packet
	{
		public Global.Map Target { get; set; }
		
		public MapComplementaryInformationsDataMessage (Global.Map target)
			: base(PacketID.MapComplementaryInformationsDataMessage)
		{
			Target = target;
		}

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target);
            return base.Pack();
        }
		
		public static void serialize(Utils.Objects.Packet sender, Global.Map map)
		{
            map.serializeAs_MapComplementaryInformationsDataMessage(sender);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static MapComplementaryInformationsDataMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

