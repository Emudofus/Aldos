using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterCreationRequestMessage : Utils.Objects.Packet
    {
        public object[] CharacterInfos { get; set; }

        public CharacterCreationRequestMessage(object[] characterInfos)
            : base(PacketID.CharacterCreationRequestMessage)
        {
            CharacterInfos = characterInfos;
        }

        public static CharacterCreationRequestMessage deserialize(Utils.Objects.Packet packet)
        {
            string name = packet.ReadUTF();
            Breed breed = (Breed)packet.ReadByte();
            Gender gender = (Gender)packet.ReadByte();

            int[] colors = new int[6];
            for (int i = 0; i < 6; ++i)
                colors[i] = packet.ReadInt();

            return new CharacterCreationRequestMessage
                (new object[]{
                    name, breed, gender, colors
                });
        }
    }
}
