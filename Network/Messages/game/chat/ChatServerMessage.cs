using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.chat
{
    class ChatServerMessage : Utils.Objects.Packet
    {
        public Channel Channel { get; set; }
        public string Message { get; set; }
        public Global.Character Actor { get; set; }

        public ChatServerMessage(Channel chan, string message, Global.Character actor)
            : base(PacketID.ChatServerMessage)
        {
            Channel = chan;
            Message = message;
            Actor = actor;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Channel, Message, Actor);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Channel chan, string message, Global.Character actor)
        {
            sender.WriteByte((byte)chan);
            sender.WriteUTF(message);
            sender.WriteInt( (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds );
            sender.WriteUTF(string.Empty);
            sender.WriteInt((int)actor.Id);
            sender.WriteUTF(actor.Name);
            sender.WriteInt((int)actor.Owner.Id);
        }
    }
}
