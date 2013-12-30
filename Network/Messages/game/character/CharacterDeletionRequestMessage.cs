using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterDeletionRequestMessage : Utils.Objects.Packet
    {
        public int CharacterID { get; set; }
        public string Reponse { get; set; }

        public CharacterDeletionRequestMessage(int characterId, string reponse)
            : base(PacketID.CharacterDeletionRequestMessage)
        {
            CharacterID = characterId;
            Reponse = reponse;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, CharacterID, Reponse);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int characterId, string reponse)
        {
            sender.WriteInt(characterId);
            sender.WriteUTF(reponse);
        }

        public static CharacterDeletionRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            return new CharacterDeletionRequestMessage(sender.ReadInt(), sender.ReadUTF());
        }
    }
}
