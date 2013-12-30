using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.friend
{
    class FriendAddRequestMessage : Utils.Objects.Packet
    {
        public string Name { get; set; }

        public FriendAddRequestMessage(string name)
            : base(PacketID.FriendAddRequestMessage)
        {
            Name = name;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Name);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string name)
        {
            sender.WriteUTF(name);
        }

        public static FriendAddRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            return new FriendAddRequestMessage(sender.ReadUTF());
        }
    }
}
