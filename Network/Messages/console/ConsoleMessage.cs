using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.console
{
    class ConsoleMessage : Utils.Objects.Packet
    {
        public Enums.ConsoleMessageTypeEnum Type { get; set; }
        public string Message { get; set; }

        public ConsoleMessage(Enums.ConsoleMessageTypeEnum type, string message)
            : base(PacketID.ConsoleMessage)
        {
            Type = type;
            Message = message;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Type, Message);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.ConsoleMessageTypeEnum type, string message)
        {
            sender.WriteByte((byte)type);
            sender.WriteUTF(message);
        }
    }
}
