using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterNameSuggestionSuccessMessage : Utils.Objects.Packet
    {
        public string Name { get; set; }

        public CharacterNameSuggestionSuccessMessage()
        {
            base.ID = PacketType.CharacterNameSuggestionSuccessMessage;

            Name = Utils.Other.RandomPseudo(6);
        }
        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Name);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet packet, string name)
        {
            packet.WriteUTF(name);
        }
        public static CharacterNameSuggestionSuccessMessage deserialize(Utils.Objects.Packet packet)
        {
            CharacterNameSuggestionSuccessMessage ret = new CharacterNameSuggestionSuccessMessage();
            ret.Name = packet.ReadUTF();
            return ret;
        }
    }
}
