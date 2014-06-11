using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class ICollectableDefaultValues
    {
        public static readonly CollisionType CollisionType = CollisionType.Gas;
        public static readonly Vector2 Position = new Vector2(0, 0);
        public static readonly Vector2 Size = new Vector2(16, 16);
        public static readonly int Duration = 0;
        public static readonly string Message = "";
    }
}
