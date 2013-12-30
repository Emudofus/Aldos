using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.character
{
	public class CharacterBaseInformations
	{
        public static int ProtocolID { get { return 45; } }

        public Global.Character Character { get; private set; }
		
		public CharacterBaseInformations(Global.Character character)
		{
			Character = character;
		}
		
		public void serialize(Utils.Objects.Packet sender)
		{
            sender.WriteInt( (int)Character.Id );
            sender.WriteByte( (byte)Character.Level );
            sender.WriteUTF(Character.Name);

            Character.Look.serialize(sender);

            sender.WriteByte( (byte)Character.Breed );
            sender.WriteByte( (byte)Character.Gender );
		}
	}
}

