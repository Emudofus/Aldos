using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class SelectedServerDataMessage : Utils.Objects.Packet
    {
        public string Ticket { get; set; }
        public string  SenderIP { get; set; }

        public SelectedServerDataMessage(string ticket, string senderIp)
            : base(PacketID.SelectedServerDataMessage)
        {
            Ticket = ticket;
            SenderIP = senderIp;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Ticket, SenderIP);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string ticket, string senderIp)
        {
            sender.WriteShort( (short)GlobalConfig.Network.Game.Guid );
            sender.WriteUTF(senderIp == "127.0.0.1" ? "127.0.0.1" : GlobalConfig.Network.IP);
            sender.WriteUShort( (ushort)GlobalConfig.Network.Game.Port );
            sender.WriteBool(true); // can create characters
            sender.WriteUTF(ticket);
        }
        /// <summary>
        /// NOT READY
        /// </summary>
        public static SelectedServerDataMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}