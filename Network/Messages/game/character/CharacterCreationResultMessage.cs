using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterCreationResultMessage : Utils.Objects.Packet
    {
        public Enums.CharacterCreationResult Reason { get; set; }

        public CharacterCreationResultMessage(Enums.CharacterCreationResult reason)
            : base(PacketID.CharacterCreationResultMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.CharacterCreationResult reason)
        {
            sender.WriteByte((byte)reason);
        }
        public static CharacterCreationResultMessage deserialize(Utils.Objects.Packet sender)
        {
            return new CharacterCreationResultMessage((Enums.CharacterCreationResult)sender.ReadByte());
        }
    }
}
