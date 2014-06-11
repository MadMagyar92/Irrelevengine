using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class CollisionContainer
    {

        public ICollidable CollidableOne { get; set; }
        public ICollidable CollidableTwo { get; set; }
        public Rectangle IntersectRect { get; set; }
        public CollisionDirection CollisionTypeOne { get; set; }
        public CollisionDirection CollisionTypeTwo { get; set; }

        public CollisionContainer(ICollidable collidableOne, ICollidable collidableTwo, Rectangle intersectRect, CollisionDirection collisionTypeOne, CollisionDirection collisionTypeTwo)
        {
            this.CollidableOne = collidableOne;
            this.CollidableTwo = collidableTwo;
            this.IntersectRect = intersectRect;
            this.CollisionTypeOne = collisionTypeOne;
            this.CollisionTypeTwo = collisionTypeTwo;
        }
    }
}
