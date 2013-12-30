using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatErrorMessage : Utils.Objects.Packet
    {
        public Enums.ChatErrorEnum Reason { get; set; }

        public ChatErrorMessage(Enums.ChatErrorEnum reason)
            : base(PacketID.ChatErrorMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.ChatErrorEnum reason)
        {
            sender.WriteByte((byte)reason);
        }
    }
}
