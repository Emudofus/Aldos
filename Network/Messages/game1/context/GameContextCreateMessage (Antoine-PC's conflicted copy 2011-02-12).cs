using System;
namespace Aldos.Network.Messages.game.context
{
	public class GameContextCreateMessage : Utils.Objects.Packet
	{
		public Enums.GameContextEnum Context {
			get;
			set;
		}
		public GameContextCreateMessage (Enums.GameContextEnum context)
			: base(PacketType.GameContextCreateMessage)
		{
			Context = context;
		}
		
		public static void serialize(Utils.Objects.Packet sender, Enums.GameContextEnum context)
		{
			sender.WriteByte((byte)context);
		}
		
		public static GameContextCreateMessage deserialize(Utils.Objects.Packet sender)
		{
			return new GameContextCreateMessage( (Enums.GameContextEnum)sender.ReadByte() );
		}
	}
}

