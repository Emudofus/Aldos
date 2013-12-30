using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.context.roleplay.party
{
    class PartyJoinMessage : Utils.Objects.Packet
    {
        public Global.Party Party { get; set; }

        public PartyJoinMessage(Global.Party party)
            : base(PacketType.PartyJoinMessage)
        {
            Party = party;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Party);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Party party)
        {
            sender.WriteInt(party.Leader.Guid);

            sender.WriteShort((short)party.Members.Count);
            foreach (Global.Character member in party.Members)
                new Types.game.context.PartyMemberInformations(member).serialize(sender);

            sender.WriteBool(false); // restricted ??
        }
    }
}
