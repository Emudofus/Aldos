using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyInvitationMessage : Utils.Objects.Packet
    {
        public Global.Character Sender { get; set; }
        public Global.Character Target { get; set; }

        public PartyInvitationMessage(Global.Character sender, Global.Character target)
            : base(PacketType.PartyInvitationMessage)
        {
            Sender = sender;
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Sender, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Character senderChar, Global.Character targetChar)
        {
            sender.WriteInt(senderChar.Guid);
            sender.WriteUTF(senderChar.Name);

            sender.WriteInt(targetChar.Guid);
            sender.WriteUTF(targetChar.Name);
        }
    }
}
