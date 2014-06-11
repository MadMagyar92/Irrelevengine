using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public enum CollisionType { Solid, Liquid, Gas };

    public interface ICollidable
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        Vector2 Velocity { get; set; }
        CollisionType CollisionType { get; set; }
    }
}
