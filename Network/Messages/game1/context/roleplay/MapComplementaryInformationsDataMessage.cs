using System;
namespace Aldos.Network.Messages.game.context.roleplay
{
	public class MapComplementaryInformationsDataMessage : Utils.Objects.Packet
	{
		public Global.Map Target { get; set; }
		
		public MapComplementaryInformationsDataMessage (Global.Map target)
			: base(PacketType.MapComplementaryInformationsDataMessage)
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
			sender.WriteShort( (short)map.SubAreaID );
			sender.WriteInt( map.ID );
			sender.WriteByte(0); // subareaAlignmentSide
			
			sender.WriteShort(0); // nHouses
			
			sender.WriteShort( (short)map.Actors.Count );
            foreach (Global.Character actor in map.Actors)
            {
                sender.WriteShort((short)Types.game.context.GameRolePlayActorInformations.ProtocolID);
                new Types.game.context.GameRolePlayActorInformations(actor).serialize(sender);
            }
			
			sender.WriteShort(0); // nInteractiveElements
			
			sender.WriteShort(0); // nStatedElements
			
			sender.WriteShort(0); // nObstacles
			
			sender.WriteShort(0); // nFights
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

