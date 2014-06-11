using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public static class CharacterCollisionManager
    {

        public static void HandleMovementAndCollisions(AbstractCharacter character)
        {
            character.Velocity = new Vector2(character.Velocity.X, character.Velocity.Y + Constants.G_CONST);
            Vector2 newPosition = new Vector2(character.Position.X + character.Velocity.X, character.Position.Y + character.Velocity.Y);
            Vector2 finalPosition = newPosition;
            
            Vector2 itemNewPosition;
            Vector2 itemFinalPosition;

            List<ICollidable> ignoreList;
            ignoreList = new List<ICollidable>();
            if(character.CurrentItem != null)
            {
                ignoreList.Add(character);
                ignoreList.Add(character.CurrentItem);
            }

            if (CollisionDetector.IsPositionFree(character, new Vector2(character.Position.X, character.Position.Y + 1)))
            {
                character.GoToAir();
            }

            Vector2 testPosition = new Vector2(newPosition.X, character.Position.Y);
            HandleItemCollisions(character, testPosition);
            HandleCollectableCollisions(character, testPosition);
            HandleEnemyCollisions(character, testPosition);
            HandleTerrainCollisions(character, newPosition);

            if (!CollisionDetector.IsPositionFree(character, newPosition, ignoreList))
            {
                finalPosition = CollisionHelper.GetNewPosition(character, newPosition, ignoreList);

                if (finalPosition.Y < newPosition.Y)
                {
                    character.LandOnGround();
                }
                if (finalPosition.Y > newPosition.Y)
                {
                    character.HitBlockFromBelow();
                }
            }


            if (character.CurrentItem != null)
            {
                itemNewPosition = Vector2.Add(character.CurrentItem.Position, Vector2.Subtract(finalPosition, character.Position));
                itemFinalPosition = itemNewPosition;
                if (!CollisionDetector.IsPositionFree(character.CurrentItem, itemNewPosition, ignoreList))
                {
                    itemFinalPosition = CollisionHelper.GetNewPosition(character.CurrentItem, itemNewPosition, ignoreList);

                    if (itemFinalPosition.Y < itemNewPosition.Y)
                    {
                        character.LandOnGround();
                    }
                    if (itemFinalPosition.Y > itemNewPosition.Y)
                    {
                        character.HitBlockFromBelow();
                    }
                }

                float itemDistance = Vector2.Distance(itemFinalPosition, character.CurrentItem.Position);
                float characterDistance = Vector2.Distance(finalPosition, character.Position);
                if (characterDistance > itemDistance)
                {
                    finalPosition = Vector2.Add(character.Position, Vector2.Subtract(itemFinalPosition, character.CurrentItem.Position));
                }
            }

            character.Position = finalPosition;
            character.UpdateItemPosition();
        }

        private static void HandleItemCollisions(AbstractCharacter character, Vector2 newPosition)
        {
            List<IItem> itemList = CollisionDetector.GetObjectsAtPosition<IItem>(character, newPosition);
            HandlePushing(character, newPosition, itemList);
            foreach (IItem item in itemList)
            {
                RespondToIItemCollision(item, character);
            }
        }
        private static void HandleCollectableCollisions(AbstractCharacter character, Vector2 newPosition)
        {
            List<ICollectable> collectableList = CollisionDetector.GetObjectsAtPosition<ICollectable>(character, newPosition);
            foreach (ICollectable collectable in collectableList)
            {
                RespondToICollectableCollision(collectable, character);
            }
        }
        private static void HandleEnemyCollisions(AbstractCharacter character, Vector2 newPosition)
        {
            List<IEnemy> enemyList = CollisionDetector.GetObjectsAtPosition<IEnemy>(character, newPosition);
            foreach (IEnemy enemy in enemyList)
            {
                RespondToIEnemyCollision(enemy, character);
            }
        }

        private static void HandleTerrainCollisions(AbstractCharacter character, Vector2 newPosition)
        {
            /*List<LavaTerrain> terrainList = CollisionDetector.GetObjectsAtPosition<LavaTerrain>(character, newPosition);

            foreach (LavaTerrain terrain in terrainList)
            {
                RespondToTerrainCollision(terrain, character);
            }*/
        }

        private static void HandlePushing(AbstractCharacter character, Vector2 newPosition, List<IItem> pushList)
        {
            if (character.StateManager.IsInAir || character.CurrentItem != null)
            {
                return;
            }
            foreach (IItem item in pushList)
            {
                if (item.IsItemPushable() && character.Position.Y + character.Size.Y > item.Position.Y)
                {
                    Vector2 oldItemVelocity = item.Velocity;
                    item.Velocity = character.Velocity;
                    IItemCollisionManager.HandleMovement(item);
                    item.Velocity = oldItemVelocity;
                    character.StateManager.IsCollidingWithPushableObject = true;
                }
            }
        }

        //Handle what happens to the collidable when it collides with the item
        
        private static void RespondToIItemCollision(IItem item, AbstractCharacter character)
        {
            if (item is Door)
            {
                Door door = (Door)item;
                
                if (door.MessageToReceivers.Equals(Constants.NORM_LEVEL_STRING) && character.CurrentItem is FishbowlItem)
                {
                    door.HandleCollisionWithCharacter();
                    LevelManager.SendEvent(EventFactory.GetDefaultLevelCompleteEvent());
                }
                else if (door.MessageToReceivers.Equals(Constants.SUB_LEVEL_STRING))
                {
                    door.HandleCollisionWithCharacter();
                    LevelManager.SendEvent(EventFactory.GetDefaultSubLevelTransitionEvent());
                }
            }
        }

        //Handle what happens to the collidable when it collides with an item
        private static void RespondToIEnemyCollision(IEnemy enemy, AbstractCharacter character)
        {
            if (enemy.IsAlive)
            {
                character.ChangeHealthLevel(enemy.Power);
            }
        }

        private static void RespondToICollectableCollision(ICollectable powerUp, AbstractCharacter character)
        {
            if (!powerUp.HasBeenConsumed)
            {
                SoundFactory.PlayCollectibleCollectedSound();
                powerUp.SetCharacter(character);

                if (character.CurrentPowerup != null)
                {
                    ICollectable current = character.CurrentPowerup;
                    current.Destroy();
                }

                character.CurrentPowerup = powerUp;
                powerUp.Consume();
            }
        }

        private static void RespondToTerrainCollision(AbstractTerrain terrain, AbstractCharacter character)
        {
            /*if (terrain is LavaTerrain)
            {
                character.DamageCharacter(((LavaTerrain)terrain).Damage);
            }*/
        }
    }  
}
