using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class HelloConnectMessage : Utils.Objects.Packet
    {
        public string Ticket { get; set; }

        public HelloConnectMessage(string ticket)
            : base(PacketID.HelloConnectMessage)
        {
            Ticket = ticket;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Ticket);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string ticket)
        {
            sender.WriteBool(true);
            sender.WriteUTF(ticket);
        }
        public static HelloConnectMessage deserialize(Utils.Objects.Packet sender)
        {
            sender.ReadBool();
            return new HelloConnectMessage( sender.ReadUTF() );
        }
    }
}