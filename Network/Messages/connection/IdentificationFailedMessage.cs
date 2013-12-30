using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class IdentificationFailedMessage : Utils.Objects.Packet
    {
        public Enums.IdentificationFailureReason Reason { get; set; }

        public IdentificationFailedMessage(Enums.IdentificationFailureReason reason)
            : base(PacketID.IdentificationFailedMessage)
        {
            Reason = reason;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Reason);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Enums.IdentificationFailureReason reason)
        {
            sender.WriteByte((byte)reason);
        }
        public static IdentificationFailedMessage deserialize(Utils.Objects.Packet sender)
        {
            return new IdentificationFailedMessage( (Enums.IdentificationFailureReason)sender.ReadByte() );
        }
    }
}