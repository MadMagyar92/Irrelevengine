using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public interface IEnemy : ICollidable, ISender, IReceiver
    {
        int Power { get; set; }

        bool IsAlive { get; set; }

        void Kill();
    }
}
