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
			: base(PacketID.GameContextCreateMessage)
		{
			Context = context;
		}

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Context);
            return base.Pack();
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

