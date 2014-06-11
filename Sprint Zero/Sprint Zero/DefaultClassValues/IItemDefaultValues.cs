using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class IItemDefaultValues
    {
        public static readonly Vector2 Position = new Vector2(0, 0);
        public static readonly Vector2 Size = new Vector2(16, 16);
        public static readonly CollisionType CollisionType = CollisionType.Solid;
        public static readonly int NextLevel = 0;
        public static readonly int NewX = 0;
        public static readonly int NewY = 0;
        public static readonly bool WaitingForActivation = false;
        public static readonly string Message = "level";
    }
}
