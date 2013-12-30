using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
	public class CharactersListMessage : Utils.Objects.Packet
	{
		public List<Global.Character> Characters { get; set; }
		
		public CharactersListMessage(List<Global.Character> characters)
            : base(PacketID.CharactersListMessage)
		{
			Characters = characters;
		}
		
		public override Utils.Objects.ByteBuffer Pack ()
		{
			serialize(this, Characters);
			return base.Pack ();
		}
		
		public static void serialize(Utils.Objects.Packet sender, List<Global.Character> characters)
		{
            // On classe la liste dans l'ordre décroissant de la dernière utilisation
            characters = characters.OrderByDescending(x => x.LastUse).ToList();

			sender.WriteBool(false); // hasStartupActions
			sender.WriteShort( (short)characters.Count );
			
			foreach (Global.Character each in characters)
            {
                sender.WriteShort((short)Types.character.CharacterBaseInformations.ProtocolID);
				new Types.character.CharacterBaseInformations(each).serialize(sender);
			}
		}
		
		/// <summary>
		/// NOT READY
		/// </summary>
		public static CharactersListMessage deserialize(Utils.Objects.Packet sender)
		{
			return null;
		}
	}
}

