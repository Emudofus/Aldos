using System;
namespace Aldos.Network.Messages.game.context.roleplay.quest
{
	public class QuestListMessage : Utils.Objects.Packet
	{
		/// <summary>
		/// TODO
		/// </summary>
		public QuestListMessage ()
			: base(PacketType.QuestListMessage)
		{
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender)
		{
			sender.WriteShort(0); // nFinished
			sender.WriteShort(0); // nActive
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static QuestListMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

