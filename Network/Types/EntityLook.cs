using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types
{
    public class EntityLook
    {
        public Global.Character Target { get; private set; }
        public List<int> Colors { get; private set; }

        public EntityLook(Global.Character target, List<int> colors)
        {
            Target = target;
            Colors = colors;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteShort(1); // bones ID

            sender.WriteShort(1); // nSkins
            sender.WriteShort((short)((int)Target.Breed * 10 + (int)Target.Gender));

            sender.WriteShort((short)5);
            for (int i = 0; i < 5; ++i)
                sender.WriteInt(Colors[i] | (i + 1) * 0x1000000);

            sender.WriteShort(1); // nScales
            sender.WriteShort((short)140);

            sender.WriteShort(0); // nSubEntities
        }
    }
}
