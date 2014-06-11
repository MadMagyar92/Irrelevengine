using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public enum CollisionDirection { Up, Down, Left, Right };

    public static class CollisionDetector
    {
        private static List<ICollidable> solidList =        new List<ICollidable>();
        private static List<ICollidable> liquidList =       new List<ICollidable>();
        private static List<ICollidable> gasList =          new List<ICollidable>();

        private static List<ICollidable> characterList =    new List<ICollidable>();
        private static List<ICollidable> enemyList =        new List<ICollidable>();
        private static List<ICollidable> collectableList =  new List<ICollidable>();
        private static List<ICollidable> itemList =         new List<ICollidable>();
        private static List<ICollidable> terrainList =      new List<ICollidable>();
        private static List<ICollidable> itemSetList =      new List<ICollidable>();


        public static void AddCollidableList(List<ICollidable> collidableList)
        {
            foreach (ICollidable collidable in collidableList)
            {
                AddCollidable(collidable);
            }
        }

        public static void AddCollidable(ICollidable collidable)
        {
            if (collidable is AbstractCharacter)
            {
                characterList.Add(collidable);
            }
            else if (collidable is IEnemy)
            {
                enemyList.Add(collidable);
            }
            else if (collidable is ICollectable)
            {
                collectableList.Add(collidable);
            }
            else if (collidable is IItem)
            {
                itemList.Add(collidable);
            }
            else if (collidable is AbstractTerrain)
            {
                terrainList.Add(collidable);
            }
            else
            {
                return;
            }
            switch (collidable.CollisionType)
            {
                case CollisionType.Solid:
                    solidList.Add(collidable);
                    break;
                case CollisionType.Liquid:
                    liquidList.Add(collidable);
                    break;
                case CollisionType.Gas:
                    gasList.Add(collidable);
                    break;
                default:
                    //Ignore object
                    break;
            }
            
        }

        public static void RemoveCollidable(ICollidable collidable)
        {
            if (collidable is AbstractCharacter)
            {
                characterList.Remove(collidable);
            }
            else if (collidable is IEnemy)
            {
                enemyList.Remove(collidable);
            }
            else if (collidable is ICollectable)
            {
                collectableList.Remove(collidable);
            }
            else if (collidable is IItem)
            {
                itemList.Remove(collidable);
            }
            else if (collidable is AbstractTerrain)
            {
                terrainList.Remove(collidable);
            }
            else
            {
                return;
            }
            switch (collidable.CollisionType)
            {
                case CollisionType.Solid:
                    solidList.Remove(collidable);
                    break;
                case CollisionType.Liquid:
                    liquidList.Remove(collidable);
                    break;
                case CollisionType.Gas:
                    gasList.Remove(collidable);
                    break;
                default:
                    //Ignore object
                    break;
            }

        }

        public static void ClearDetector()
        {
            characterList.Clear();
            enemyList.Clear();
            collectableList.Clear();
            itemList.Clear();
            terrainList.Clear();
            itemSetList.Clear();
            solidList.Clear();
            liquidList.Clear();
            gasList.Clear();
        }

        public static bool IsPositionFree(ICollidable collidable, Vector2 newPosition)
        {
            if (collidable.CollisionType == CollisionType.Solid)
            {
                if (IsCollidableIntersectingList(collidable, newPosition, liquidList))
                {
                    return false;
                }
            }
            if (collidable.CollisionType == CollisionType.Solid || collidable.CollisionType == CollisionType.Liquid)
            {
                if (IsCollidableIntersectingList(collidable, newPosition, solidList))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsPositionFree(ICollidable collidable, Vector2 newPosition, List<ICollidable> ignoreList)
        {
            if (collidable.CollisionType == CollisionType.Solid)
            {
                if (IsCollidableIntersectingList(collidable, newPosition, liquidList, ignoreList))
                {
                    return false;
                }
            }
            if (collidable.CollisionType == CollisionType.Solid || collidable.CollisionType == CollisionType.Liquid)
            {
                if (IsCollidableIntersectingList(collidable, newPosition, solidList, ignoreList))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCollidableIntersectingList(ICollidable collidable, Vector2 position, List<ICollidable> list)
        {
            Vector2 size = collidable.Size;
            foreach (ICollidable other in list)
            {
                if (collidable != other && DoPositionsIntersect(position, size, other.Position, other.Size))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCollidableIntersectingList(ICollidable collidable, Vector2 position, List<ICollidable> list, List<ICollidable> ignoreList)
        {
            Vector2 size = collidable.Size;
            foreach (ICollidable other in list)
            {
                if (!ignoreList.Contains(other) && collidable != other && DoPositionsIntersect(position, size, other.Position, other.Size))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DoPositionsIntersect(Vector2 onePosition, Vector2 oneSize, Vector2 twoPosition, Vector2 twoSize)
        {
            if (onePosition.Y + oneSize.Y > twoPosition.Y &&
               onePosition.Y < twoPosition.Y + twoSize.Y &&
               onePosition.X + oneSize.X > twoPosition.X &&
               onePosition.X < twoPosition.X + twoSize.X)
            {
                return true;
            }

            return false;
        }
        
        public static List<T> GetObjectsAtPosition<T>(ICollidable collidable, Vector2 newPosition) where T : ICollidable
        {
            List<T> collidables = new List<T>();
            foreach (ICollidable other in solidList)
            {
                if (other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            foreach (ICollidable other in liquidList)
            {
                if (other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            foreach (ICollidable other in gasList)
            {
                if (other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            return collidables;
        }

        public static List<T> GetObjectsAtPosition<T>(ICollidable collidable, Vector2 newPosition, List<ICollidable> ignoreList) where T : ICollidable
        {
            List<T> collidables = new List<T>();
            foreach (ICollidable other in solidList)
            {
                if (!ignoreList.Contains(other) && other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            foreach (ICollidable other in liquidList)
            {
                if (!ignoreList.Contains(other) && other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            foreach (ICollidable other in gasList)
            {
                if (!ignoreList.Contains(other) && other is T && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add((T)other);
                }
            }
            return collidables;
        }

        public static List<ICollidable> GetCollisionsAtPosition(ICollidable collidable, Vector2 newPosition)
        {
            List<ICollidable> collidables = new List<ICollidable>();
            foreach (ICollidable other in solidList)
            {
                if (other != collidable && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add(other);
                }
            }
            if (collidable.CollisionType == CollisionType.Liquid)
            {
                return collidables; //Early break in order to not test liquid vs liquid
            }
            foreach (ICollidable other in liquidList)
            {
                if (other != collidable && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add(other);
                }
            }
            return collidables;
        }

        public static List<ICollidable> GetCollisionsAtPosition(ICollidable collidable, Vector2 newPosition, List<ICollidable> ignoreList)
        {
            List<ICollidable> collidables = new List<ICollidable>();
            foreach (ICollidable other in solidList)
            {
                if (!ignoreList.Contains(other) && other != collidable && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add(other);
                }
            }
            if (collidable.CollisionType == CollisionType.Liquid)
            {
                return collidables; //Early break in order to not test liquid vs liquid
            }
            foreach (ICollidable other in liquidList)
            {
                if (!ignoreList.Contains(other) && other != collidable && DoPositionsIntersect(newPosition, collidable.Size, other.Position, other.Size))
                {
                    collidables.Add(other);
                }
            }
            return collidables;
        }
    }
}
