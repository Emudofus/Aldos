using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.game.character
{
	public class CharacterBaseInformations
	{
        public static int ProtocolID { get { return 45; } }

		private Global.Character _character;
		
		public CharacterBaseInformations(Global.Character character)
		{
			_character = character;
		}
		
		public void serialize(Utils.Objects.Packet sender)
		{
            // --------------- //
            // --- MINIMAL --- //
            // --------------- //

            sender.WriteInt(_character.Guid);
            sender.WriteByte( (byte)_character.Level );
            sender.WriteUTF(_character.Name);

            // ------------ //
            // --- LOOK --- //
            // ------------ //

            sender.WriteShort(1); // bones ID

            sender.WriteShort(1); // nSkins
            sender.WriteShort( (short)( (int)_character.Classe * 10 + (int)_character.Sexe) );

            sender.WriteShort((short)5);
            for (int i = 0; i < 5; ++i)
                sender.WriteInt(_character.Colors[i] | (i + 1) * 0x1000000);

            sender.WriteShort(1); // nScales
            sender.WriteShort( (short)140 );

            sender.WriteShort(0); // nSubEntities

            // ------------ //
            // --- BASE --- //
            // ------------ //

            sender.WriteByte( (byte)_character.Classe );
            sender.WriteByte( (byte)_character.Sexe );
		}
	}
}

