using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages.game.basic
{
    public class SystemChatMessage : TextInformationMessage
    {
        public SystemChatMessage(string message)
            : base(Enums.TextInformationType.INFORMATION_MESSAGE, message)
        {
        }
    }
}
