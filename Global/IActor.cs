using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Global
{
    public interface IActor
    {
        int Id { get; set; }
        string Name { get; set; }

        Network.Types.EntityLook Look { get; set; }
        Network.Types.context.EntityDispositionInformations Disposition { get; set; }
    }
}
