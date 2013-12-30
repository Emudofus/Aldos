using System;

namespace Aldos.Network.Messages.game.character
{
	public class CharacterSelectedSuccessMessage : Utils.Objects.Packet
	{
		public Global.Character Character { get; set; }

		public CharacterSelectedSuccessMessage (Global.Character perso)
			: base(PacketID.CharacterSelectedSuccessMessage)
		{
			Character = perso;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Character);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, Global.Character perso)
		{
			new Types.character.CharacterBaseInformations(perso).serialize(sender);
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static CharacterSelectedSuccessMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

