using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat.channel
{
    class ChannelEnablingMessage : Utils.Objects.Packet
    {
        public Channel Target { get; set; }
        public bool Enable { get; set; }

        public ChannelEnablingMessage(Channel target, bool enable)
            : base(PacketID.ChannelEnablingMessage)
        {
            Target = target;
            Enable = enable;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target, Enable);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Channel target, bool enable)
        {
            sender.WriteByte((byte)target);
            sender.WriteBool(enable);
        }

        public static ChannelEnablingMessage deserialize(Utils.Objects.Packet sender)
        {
            return new ChannelEnablingMessage((Channel)sender.ReadByte(), sender.ReadBool());
        }
    }
}
