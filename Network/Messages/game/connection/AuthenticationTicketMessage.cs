using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.connection
{
    public class AuthenticationTicketMessage : Utils.Objects.Packet
    {
        public string Ticket { get; set; }

        public AuthenticationTicketMessage(string ticket)
            : base(PacketID.AuthenticationTicketMessage)
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
            sender.WriteUTF("fr"); // LANG
            sender.WriteUTF(ticket);
        }
        public static AuthenticationTicketMessage deserialize(Utils.Objects.Packet sender)
        {
            sender.ReadUTF(); // LANG
            return new AuthenticationTicketMessage( sender.ReadUTF() );
        }
    }
}