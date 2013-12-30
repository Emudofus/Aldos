using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatClientMultiMessage : Utils.Objects.Packet
    {
        public string Message { get; set; }
        public Channel Channel { get; set; }

        public ChatClientMultiMessage(string message, Channel channel)
            : base(PacketType.ChatClientMultiMessage)
        {
            Message = message;
            Channel = channel;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Message, Channel);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, string message, Channel channel)
        {
            sender.WriteUTF(message);
            sender.WriteByte((byte)channel);
        }

        public static ChatClientMultiMessage deserialize(Utils.Objects.Packet sender)
        {
            return new ChatClientMultiMessage(sender.ReadUTF(), (Channel)sender.ReadByte());
        }
    }
}
