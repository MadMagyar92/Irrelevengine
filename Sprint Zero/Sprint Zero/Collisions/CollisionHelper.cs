using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Completely_Irrelevant
{
    public static class CollisionHelper
    {
        public static bool IsSpiderManEnabled { get { return false; } }

        public static Vector2 GetNewPosition(ICollidable collidable, Vector2 newPosition, List<ICollidable> ignoreList = null)
        {
            Vector2 guessedPosition, finalPosition;
            finalPosition = new Vector2(collidable.Position.X, collidable.Position.Y);
            guessedPosition = finalPosition;
            float distance = Vector2.Distance(collidable.Position, newPosition);
            int repititions = (int)distance + 10;
            float xDifference = (newPosition.X - collidable.Position.X) / repititions;
            float yDifference = (newPosition.Y - collidable.Position.Y) / repititions;

            for (int i = 0; i < repititions; i++)
            {
                Vector2 offset = new Vector2(xDifference, yDifference);
                Vector2 collisionDirection = CollisionDirectionFromOffset(collidable, guessedPosition, offset, ignoreList);

                if (collisionDirection == Vector2.Zero)
                {
                    guessedPosition = Vector2.Add(guessedPosition, offset);
                }
                else
                {
                    if (collisionDirection.X != 0 && xDifference != 0)
                    {
                        guessedPosition = AbsoluteFixOffsetCollision(collidable, guessedPosition, new Vector2(xDifference, 0), ignoreList);
                        xDifference = 0;
                    }
                    if (collisionDirection.Y != 0 && yDifference != 0)
                    {
                        guessedPosition = AbsoluteFixOffsetCollision(collidable, guessedPosition, new Vector2(0, yDifference), ignoreList);
                        yDifference = 0;
                    }
                    guessedPosition = new Vector2(guessedPosition.X + xDifference, guessedPosition.Y + yDifference);
                }
            }



            
            float finalX = guessedPosition.X;
            float finalY = guessedPosition.Y;
            if (xDifference != 0)
            {
                finalX = newPosition.X;
            }
            if (yDifference != 0)
            {
                finalY = newPosition.Y;
            }

            if (IsSpiderManEnabled)
            {
                //No longer good
                finalPosition = guessedPosition;
            }
            else
            {
                finalPosition = new Vector2(finalX, finalY);
            }

            return finalPosition;
        }

        private static Vector2 AbsoluteFixOffsetCollision(ICollidable collidable, Vector2 guessedPosition, Vector2 offset, List<ICollidable> ignoreList)
        {
            List<ICollidable> otherList;
            if (ignoreList == null)
            {
                otherList = CollisionDetector.GetCollisionsAtPosition(collidable, Vector2.Add(guessedPosition, offset));
            }
            else
            {
                otherList = CollisionDetector.GetCollisionsAtPosition(collidable, Vector2.Add(guessedPosition, offset), ignoreList);
            }

            int xDirection = -Math.Sign(offset.X);
            int yDirection = -Math.Sign(offset.Y);
            foreach (ICollidable other in otherList)
            {
                Vector2 tempCollisionPosition;
                Vector2 tempCollisionSize;
                OutCollisionRectangle(collidable, Vector2.Add(guessedPosition, offset), other, out tempCollisionPosition, out tempCollisionSize);
                if (tempCollisionSize != Vector2.Zero)
                {
                    guessedPosition = MoveOutOfOtherInDirection(collidable, offset, other, tempCollisionSize);
                }
            }

            return guessedPosition;
        }

        private static void OutCollisionRectangle(ICollidable collidable1, Vector2 newPosition, ICollidable collidable2, out Vector2 position, out Vector2 size)
        {
            if (!CollisionDetector.DoPositionsIntersect(newPosition, collidable1.Size, collidable2.Position, collidable2.Size))
            {
                position = size = new Vector2();
                return;
            }

            float x1 = Math.Max(newPosition.X, collidable2.Position.X);
            float y1 = Math.Max(newPosition.Y, collidable2.Position.Y);
            float x2 = Math.Min(newPosition.X + collidable1.Size.X, collidable2.Position.X + collidable2.Size.X);
            float y2 = Math.Min(newPosition.Y + collidable1.Size.Y, collidable2.Position.Y + collidable2.Size.Y);

            position = new Vector2(x1, y1);
            size = new Vector2(Math.Abs(x2-x1), Math.Abs(y2-y1));
        }

        private static Vector2 MoveOutOfOtherInDirection(ICollidable collidable, Vector2 offset, ICollidable other, Vector2 collisionSize)
        {
            Vector2 moveOutDirection;


            float xComp, yComp;
            //.01f decided to be a small epsilon to handle the case where a collision is extremely small in one direction.
            xComp = (offset.X + .01f) / (collisionSize.X == 0 ? float.MaxValue : collisionSize.X);
            yComp = (offset.Y + .01f) / (collisionSize.Y == 0 ? float.MaxValue : collisionSize.Y);

            if (Math.Abs(xComp) > Math.Abs(yComp))
            {
                moveOutDirection = new Vector2(-offset.X / collisionSize.X, 0);
            }
            else
            {
                moveOutDirection = new Vector2(0, -offset.Y / collisionSize.Y);
            }


            if (moveOutDirection.X > 0)
            {
                return new Vector2(other.Position.X + other.Size.X, collidable.Position.Y);
            }
            else if (moveOutDirection.X < 0)
            {
                return new Vector2(other.Position.X - collidable.Size.X, collidable.Position.Y);
            }
            else if (moveOutDirection.Y > 0)
            {
                return new Vector2(collidable.Position.X, other.Position.Y + other.Size.Y);
            }
            else if (moveOutDirection.Y < 0)
            {
                return new Vector2(collidable.Position.X, other.Position.Y - collidable.Size.Y);
            }
            else
            {
                return collidable.Position;
            }
        }

        private static Vector2 CollisionDirectionFromOffset(ICollidable collidable, Vector2 testPosition, Vector2 offset, List<ICollidable> ignoreList)
        {
            if (collidable is AbstractCharacter)
            {

            }
            Vector2 collisionDirection = new Vector2();
            Vector2 tempPosition = new Vector2(testPosition.X + offset.X, testPosition.Y + offset.Y);
            if (GetPositionFree(collidable, tempPosition, ignoreList))
            {
                return collisionDirection;
            }
            //else
            if(offset.X != 0 && !GetPositionFree(collidable, new Vector2(testPosition.X + offset.X, testPosition.Y), ignoreList))
            {
                collisionDirection = new Vector2(offset.X, collisionDirection.Y);
            }
            if (offset.Y != 0 && !GetPositionFree(collidable, new Vector2(testPosition.X, testPosition.Y + offset.Y), ignoreList))
            {
                collisionDirection = new Vector2(collisionDirection.X, offset.Y);
            }
            return collisionDirection;
        }

        private static bool GetPositionFree(ICollidable collidable, Vector2 newPosition, List<ICollidable> ignoreList)
        {
            if (ignoreList == null)
            {
                return CollisionDetector.IsPositionFree(collidable, newPosition);
            }
            else
            {
                return CollisionDetector.IsPositionFree(collidable, newPosition, ignoreList);
            }
        }

        public static Vector2 GetCollisionVector(Vector2 finalPosition, Vector2 targetPosition)
        {
            int left, right, up, down; //Collision Directions
            left = right = up = down = 0;
            if (finalPosition.X > targetPosition.X)
            {
                left = -1;
            }
            else if (finalPosition.X < targetPosition.X)
            {
                right = 1;
            }
            if (finalPosition.Y > targetPosition.Y)
            {
                up = -1;
            }
            else if (finalPosition.Y < targetPosition.Y)
            {
                down = 1;
            }
            return new Vector2(left + right, up + down);
        }

        public static bool IsLeftVector(Vector2 collisionDirection)
        {
            return collisionDirection.X < 0;
        }
        public static bool IsRightVector(Vector2 collisionDirection)
        {
            return collisionDirection.X > 0;
        }
        public static bool IsUpVector(Vector2 collisionDirection)
        {
            return collisionDirection.Y < 0;
        }
        public static bool IsDownVector(Vector2 collisionDirection)
        {
            return collisionDirection.Y > 0;
        }
    }
}
