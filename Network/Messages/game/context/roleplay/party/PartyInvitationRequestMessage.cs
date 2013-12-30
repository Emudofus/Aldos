using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyInvitationRequestMessage : Utils.Objects.Packet
    {
        public Global.Character Target { get; set; }

        public PartyInvitationRequestMessage(Global.Character target)
            : base(PacketID.PartyInvitationRequestMessage)
        {
            Target = target;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Target);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Character target)
        {
            sender.WriteUTF(target.Name);
        }

        public static PartyInvitationRequestMessage deserialize(Utils.Objects.Packet sender)
        {
            string target = sender.ReadUTF();
            return new PartyInvitationRequestMessage(Global.Character.FindOne(chr => chr.Name == target));
        }
    }
}
