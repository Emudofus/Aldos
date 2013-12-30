using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Utils.Objects
{
    public class GameEventArgs : EventArgs
    {
        public GameEventArgsType TypeArg { get; private set; }

        public GameEventArgs(GameEventArgsType type)
        {
            TypeArg = type;
        }
    }
}
