using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.connection
{
    class ServerSelectionMessage : Utils.Objects.Packet
    {
        public int ChosenServer { get; set; }

        public ServerSelectionMessage(int chosenServer)
            : base(PacketID.ServerSelectionMessage)
        {
            ChosenServer = chosenServer;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, ChosenServer);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, int chosenServer)
        {
            sender.WriteUShort( (ushort)chosenServer );
        }
        public static ServerSelectionMessage deserialize(Utils.Objects.Packet sender)
        {
            return new ServerSelectionMessage( sender.ReadUShort() );
        }
    }
}