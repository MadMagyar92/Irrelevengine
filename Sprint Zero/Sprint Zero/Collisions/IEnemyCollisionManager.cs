using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public static class IEnemyCollisionManager
    {
        public static void HandleMovement(IEnemy enemy)
        {
            Vector2 targetPosition = new Vector2(enemy.Position.X + enemy.Velocity.X, enemy.Position.Y + enemy.Velocity.Y);
            Vector2 finalPosition = targetPosition;

            if (!CollisionDetector.IsPositionFree(enemy, targetPosition))
            {
                finalPosition = CollisionHelper.GetNewPosition(enemy, targetPosition);
                HandleCollisionDirections(enemy, finalPosition, targetPosition);
            }



            enemy.Position = finalPosition;
        }



        public static void HandleCollisionDirections(IEnemy enemy, Vector2 finalPosition, Vector2 targetPosition)
        {
            Vector2 collisionDirection = CollisionHelper.GetCollisionVector(finalPosition, targetPosition);
            bool left, right, up, down;
            left = CollisionHelper.IsLeftVector(collisionDirection);
            right = CollisionHelper.IsRightVector(collisionDirection);
            up = CollisionHelper.IsUpVector(collisionDirection);
            down = CollisionHelper.IsDownVector(collisionDirection);

            if (enemy is HortoiseEnemy && (left || right))
            {
                ((HortoiseEnemy)enemy).ChangeDirection();
            }
            else if (enemy is SpiderEnemy && (up || down))
            {
                ((SpiderEnemy)enemy).ChangeDirection();
            }
            else if (enemy is NeedleEnemy)
            {
                ((NeedleEnemy)enemy).Kill();
            }
        }
    }
}
