using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public interface ISender : IUpdatable, IDrawable
    {
        bool ShouldNotifyReceivers { get; set; }
        string MessageToReceivers { get; set; }
    }
}
