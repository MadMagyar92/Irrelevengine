using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public static class IItemCollisionManager
    {
        public static void HandleMovement(IItem item)
        {
            item.Velocity = new Vector2(item.Velocity.X, item.Velocity.Y + Constants.G_CONST);
            Vector2 targetPosition = new Vector2(item.Position.X + item.Velocity.X, item.Position.Y + item.Velocity.Y);
            Vector2 finalPosition = targetPosition;

            if (!CollisionDetector.IsPositionFree(item, targetPosition))
            {
                if (item.Velocity.Y >= Constants.CRUSHING_SPD_CONST)
                {
                    List<IEnemy> crushList = CollisionDetector.GetObjectsAtPosition<IEnemy>(item, targetPosition);
                    foreach (IEnemy enemy in crushList)
                    {
                        enemy.Kill();
                        ScoreManager.IncreaseScore(Constants.CRUSHING_PTS_CONST);
                    }
                }
                finalPosition = CollisionHelper.GetNewPosition(item, targetPosition);
                HandleCollisionDirections(item, finalPosition, targetPosition);
            }



            item.Position = finalPosition;
        }

        public static void HandleCollisionDirections(IItem item, Vector2 finalPosition, Vector2 targetPosition)
        {
            Vector2 collisionDirection = CollisionHelper.GetCollisionVector(finalPosition, targetPosition);
            bool left, right, up, down;
            left = CollisionHelper.IsLeftVector(collisionDirection);
            right = CollisionHelper.IsRightVector(collisionDirection);
            up = CollisionHelper.IsUpVector(collisionDirection);
            down = CollisionHelper.IsDownVector(collisionDirection);

            if (item is PickupItem && down)
            {
                ((PickupItem)item).FloorCollision();
            }
            if (item is CrateItem && down)
            {
                ((CrateItem)item).FloorCollision();
            }
        }

    }
}
