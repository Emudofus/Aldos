using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Types.game.friend
{
    public class FriendInformations
    {
        public int ProtocolID
        {
            get
            {
                if (Friend.CurrentCharacter != null && Friend.CurrentCharacter.IsConnected)
                    return 92;
                return 78;
            }
        }

        public Global.Account Friend { get; set; }

        public FriendInformations(Global.Account friend)
        {
            Friend = friend;
        }

        public void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteUTF(Friend.Nickname);
            sender.WriteByte(Friend.CurrentCharacter != null ? (byte)Friend.CurrentCharacter.State : (byte)Enums.PlayerStateEnum.NOT_CONNECTED);
            sender.WriteInt(0);

            if (Friend.CurrentCharacter != null && Friend.CurrentCharacter.IsConnected)
            {
                sender.WriteUTF(Friend.CurrentCharacter.Name);
                sender.WriteShort((short)Friend.CurrentCharacter.Level);
                sender.WriteByte(0); // alignmentSide
                sender.WriteByte((byte)Friend.CurrentCharacter.Classe);
                sender.WriteByte((byte)Friend.CurrentCharacter.Sexe);

                sender.WriteInt(0); // guild guid
                sender.WriteUTF(""); // guild name

                sender.WriteByte(0xff); // mood smiley id
            }
        }
    }
}
