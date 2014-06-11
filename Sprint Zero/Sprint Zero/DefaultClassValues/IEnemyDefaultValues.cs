using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class IEnemyDefaultValues
    {
        public static readonly SpriteOrientation Orientation = SpriteOrientation.Up;
        public static readonly CollisionType CollisionType = CollisionType.Liquid;
        public static readonly Vector2 Position = new Vector2(0, 0);
        public static readonly Vector2 Size = new Vector2(16, 16);
        public static readonly int Power = 0;
        public static readonly int Speed = 0;
        public static readonly int Frequency = 0;
        public static readonly string Message = "";
    }
}
