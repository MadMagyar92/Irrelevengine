using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public interface IReceiver : IUpdatable, IDrawable
    {
        void Receive(List<string> messages);
    }
}
