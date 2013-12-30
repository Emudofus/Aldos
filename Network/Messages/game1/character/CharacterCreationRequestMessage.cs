using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterCreationRequestMessage : Utils.Objects.Packet
    {
        public Global.Character Character { get; set; }

        public CharacterCreationRequestMessage(Global.Character character)
        {
            base.ID = PacketType.CharacterCreationRequestMessage;

            Character = character;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet packet, Global.Character character)
        {
            packet.WriteUTF(character.Name);
            packet.WriteByte((byte)character.Classe);
            packet.WriteByte((byte)character.Sexe);

            foreach (int color in character.Colors)
                packet.WriteInt(color);
        }
        public static CharacterCreationRequestMessage deserialize(Utils.Objects.Packet packet)
        {
            string name = packet.ReadUTF();
            Classe classe = (Classe)packet.ReadByte();
            Sexe sexe = (Sexe)packet.ReadByte();

            int[] colors = new int[6];
            for (int i = 0; i < 6; ++i)
                colors[i] = packet.ReadInt();

            return new CharacterCreationRequestMessage
                (
                    new Global.Character
                        (
                            name, classe, sexe, colors
                        )
                );
        }
    }
}
