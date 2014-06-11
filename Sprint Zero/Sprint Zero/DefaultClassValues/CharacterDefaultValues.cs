using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public static class CharacterDefaultValues
    {
        public static readonly Vector2 Position = new Vector2(0, 0);
        public static readonly Vector2 Size = new Vector2(16, 16);
        public static readonly CollisionType CollisionType = CollisionType.Liquid;
        public static readonly int HealthLevel = 4;
        public static readonly int MeterLevel = 4;
        public static readonly int DehydrationDamage = 1;
        public static readonly int DehydrationTime = 15;
        public static readonly bool DehydrationEnabled = true;
        public static readonly string Message = "";
    }
}
