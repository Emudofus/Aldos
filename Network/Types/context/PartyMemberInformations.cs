using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.game.context
{
    class PartyMemberInformations
    {
        public Global.Character Member { get; set; }

        public PartyMemberInformations(Global.Character member)
        {
            Member = member;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            // MINIMAL INFO
            sender.WriteInt((int)Member.Id);
            sender.WriteByte((byte)Member.Level);
            sender.WriteUTF(Member.Name);

            Member.Look.serialize(sender);

            // PARTY
            sender.WriteInt(Member.Characteristics.lifePoints);
            sender.WriteInt(Member.Characteristics.maxLifePoints);
            sender.WriteShort((short)Member.Characteristics.prospecting.Total);
            sender.WriteByte(0); // regenRate
            sender.WriteShort((short)Member.Characteristics.initiative.Total);
            sender.WriteBool(false); // pvp enabled
            sender.WriteByte(0); // alignment side
        }
    }
}
