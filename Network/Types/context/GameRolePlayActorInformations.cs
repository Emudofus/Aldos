using System;
namespace Aldos.Network.Types.context
{
	public class GameRolePlayActorInformations
	{
		public static int ProtocolID { get { return 141; } }
		
		private Global.IActor _target;
		
		public GameRolePlayActorInformations (Global.IActor target)
		{
			_target = target;
		}
		
		public void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteInt(_target.Id);

            _target.Look.serialize(sender);

            sender.WriteShort((short)EntityDispositionInformations.ProtocolID);
            _target.Disposition.serialize(sender);
		}
	}
}

