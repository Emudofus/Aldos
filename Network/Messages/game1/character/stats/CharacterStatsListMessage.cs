using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.character.stats
{
    public class CharacterStatsListMessage : Utils.Objects.Packet
    {
        public Global.Character Character { get; set; }

        public CharacterStatsListMessage(Global.Character character)
            : base(PacketType.CharacterStatsListMessage)
        {
            Character = character;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this, Character);
            return base.Pack();
        }

        public static void serialize(Utils.Objects.Packet sender, Global.Character character)
        {
            character.Characteristics.serialize(sender);
        }

        /// <summary>
        /// NOT READY
        /// </summary>
        public static CharacterStatsListMessage deserialize(Utils.Objects.Packet sender)
        {
            return null;
        }
    }
}
