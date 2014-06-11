using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public static class CollectableCollisionManager
    {
        private static List<CollisionContainer> collisionList = new List<CollisionContainer>();

        public static void ManageCollisions(Level level)
        {

            collisionList.Clear();
        }

        public static void AddCollision(CollisionContainer collision)
        {
            //collisionList.Add(collision);
        }
    }
}
