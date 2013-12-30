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
            sender.WriteInt(Member.Guid);
            sender.WriteByte((byte)Member.Level);
            sender.WriteUTF(Member.Name);

            // LOOK
            sender.WriteShort(1); // bones ID

            sender.WriteShort(1); // nSkins
            sender.WriteShort((short)((int)Member.Classe * 10 + (int)Member.Sexe));

            sender.WriteShort((short)5);
            for (int i = 0; i < 5; ++i)
                sender.WriteInt(Member.Colors[i] | (i + 1) * 0x1000000);

            sender.WriteShort(1); // nScales
            sender.WriteShort((short)140);

            sender.WriteShort(0); // nSubEntities

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
