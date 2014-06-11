using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class BoulderItem : CrateItem
    {
        public BoulderItem(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, string message)
            :base(position, sprite, collisionType, message)
        {

        }
    }
}
