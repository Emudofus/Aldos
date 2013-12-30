using System;
namespace Aldos.Network.Types.game.context
{
	public class GameRolePlayActorInformations
	{
		public static int ProtocolID { get { return 141; } }
		
		private Global.Character _target;
		
		public GameRolePlayActorInformations (Global.Character target)
		{
			_target = target;
		}
		
		public void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteInt(_target.Guid);
			
			////// ENTITY LOOK //////
			
			sender.WriteShort(1); // bones ID

            sender.WriteShort(1); // nSkins
            sender.WriteShort( (short)( (int)_target.Classe * 10 + (int)_target.Sexe) );

            sender.WriteShort((short)5);
            for (int i = 0; i < 5; ++i)
                sender.WriteInt(_target.Colors[i] | (i + 1) * 0x1000000);

            sender.WriteShort(1); // nScales
            sender.WriteShort( (short)140 );

            sender.WriteShort(0); // nSubEntities
			
			////// ENTITY DISPOSITION

            sender.WriteShort((short)Types.game.context.EntityDispositionInformations.ProtocolID);
            _target.Disposition.serialize(sender);
		}
	}
}

