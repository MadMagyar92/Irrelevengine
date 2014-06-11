using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public interface IController
    {
        void Update();
        bool IsActionActive(Actions action);
        bool IsConnected();
    }
}
