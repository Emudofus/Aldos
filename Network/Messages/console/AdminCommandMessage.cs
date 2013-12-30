using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.console
{
    class AdminCommandMessage : Utils.Objects.Packet
    {
        public string Message { get; set; }

        public AdminCommandMessage(string message)
            : base(PacketID.AdminCommandMessage)
        {
            Message = message;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Message);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string message)
        {
            sender.WriteUTF(message);
        }

        public static AdminCommandMessage deserialize(Utils.Objects.Packet sender)
        {
            return new AdminCommandMessage(sender.ReadUTF());
        }
    }
}
