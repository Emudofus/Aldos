using System;
namespace Aldos.Network.Messages.game.character
{
	public class CharacterSelectionMessage : Utils.Objects.Packet
	{
		public int Chosen {
			get;
			set;
		}
		
		public CharacterSelectionMessage (int chosen)
			: base(PacketID.CharacterSelectionMessage)
		{
			Chosen = chosen;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Chosen);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, int chosen)
		{
			sender.WriteInt(chosen);
		}
		
		public static CharacterSelectionMessage deserialize(Utils.Objects.Packet sender)
		{
			return new CharacterSelectionMessage(sender.ReadInt());
		}
	}
}

