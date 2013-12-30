using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character
{
    class CharacterDeletionErrorMessage : Utils.Objects.Packet
    {
        public Network.Enums.CharacterDeletionErrorEnum Reason { get; set; }

        public CharacterDeletionErrorMessage(Network.Enums.CharacterDeletionErrorEnum reason)
            : base(PacketID.CharacterDeletionErrorMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Network.Enums.CharacterDeletionErrorEnum reason)
        {
            sender.WriteByte((byte)reason);
        }
        public static CharacterDeletionErrorMessage deserialize(Utils.Objects.Packet sender)
        {
            return new CharacterDeletionErrorMessage((Network.Enums.CharacterDeletionErrorEnum)sender.ReadByte());
        }
    }
}
