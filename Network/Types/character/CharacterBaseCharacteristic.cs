using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.character
{
    public class CharacterBaseCharacteristic
    {
        public static int ProtocolID { get { return 4; } }

        public int Base { get; set; }
        public int ObjectsAndMountBonus { get; set; }
        public int AlignGiftBonus { get; set; }
        public int ContextModif { get; set; }
        public int Total
        {
            get
            {
                return Base + ObjectsAndMountBonus + AlignGiftBonus + ContextModif;
            }
        }

        public CharacterBaseCharacteristic() { }
        public CharacterBaseCharacteristic(int aBase, int objects, int align, int context)
        {
            Base                    = aBase;
            ObjectsAndMountBonus    = objects;
            AlignGiftBonus          = align;
            ContextModif            = context;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteShort((short)Base);
            sender.WriteShort((short)ObjectsAndMountBonus);
            sender.WriteShort((short)AlignGiftBonus);
            sender.WriteShort((short)ContextModif);
        }
    }
}
