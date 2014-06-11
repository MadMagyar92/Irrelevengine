using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class Conveyor : ICollidable, IReceiver, IItem
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public bool ShouldNotifyReceivers { get; set; }

        public bool IsItemPushable() { return false; }
        public bool IsItemInteractable() { return false; }
        public bool IsItemPickable() { return false; }

        public float speed;
        public float activatedSpeed;

        private int directionMultiplier;
        private int activatedDirectionMultiplier;

        private bool active;

        private ConveyorSprite sprite;
        private Hitbox hitboxTop;
        private Hitbox hitboxBot;

        
        public Conveyor(Vector2 position, int blockWidth, float speed, SpriteOrientation orientation, float activatedSpeed, SpriteOrientation activatedOrientation, ConveyorSprite sprite)
        {
            this.Position = position;
            this.Velocity = new Vector2();
            this.CollisionType = CollisionType.Solid;

            this.sprite = sprite;
            this.Size = sprite.GetSize();
            this.hitboxTop = new Hitbox(new Vector2(this.Position.X, this.Position.Y - 1), this.Size, Vector2.Zero, CollisionType.Gas);
            this.hitboxBot = new Hitbox(new Vector2(this.Position.X, this.Position.Y + 1), this.Size, Vector2.Zero, CollisionType.Gas);
            this.speed = speed;
            this.activatedSpeed = activatedSpeed;

            this.directionMultiplier = GetDirectionMultiplier(orientation);
            this.activatedDirectionMultiplier = GetDirectionMultiplier(activatedOrientation);

            this.active = false;
        }

        private int GetDirectionMultiplier(SpriteOrientation orientation)
        {
            if (orientation == SpriteOrientation.Clockwise)
            {
                return 1;
            }
            else if (orientation == SpriteOrientation.CounterClockwise)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }


        private int ActiveMultiplier()
        {
            return active ? activatedDirectionMultiplier : directionMultiplier;
        }
        private float ActiveSpeed()
        {
            return active ? activatedSpeed : speed;
        }
        private float TotalSpeed()
        {
            return ActiveSpeed() * ActiveMultiplier();
        }


        public void Update()
        {
            List<ICollidable> collidableList;
            sprite.Update();
            collidableList = CollisionDetector.GetCollisionsAtPosition(hitboxTop, hitboxTop.Position);
            MoveColliders(collidableList, TotalSpeed());
            collidableList = CollisionDetector.GetCollisionsAtPosition(hitboxBot, hitboxBot.Position);
            MoveColliders(collidableList, TotalSpeed());
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }



        private void MoveColliders(List<ICollidable> collidableList, float moveSpeed)
        {
            foreach (ICollidable collidable in collidableList)
            {
                Vector2 oldVelocity = collidable.Velocity;
                collidable.Velocity = new Vector2(moveSpeed, 0);
                if (collidable is AbstractCharacter)
                {
                    CharacterCollisionManager.HandleMovementAndCollisions((AbstractCharacter)collidable);
                }
                else if (collidable is FishbowlItem)
                {
                    IItemCollisionManager.HandleMovement((FishbowlItem)collidable);
                }
                else if (collidable is CrateItem)
                {
                    IItemCollisionManager.HandleMovement((CrateItem)collidable);
                }
                else if (collidable is IEnemy)
                {
                    IEnemyCollisionManager.HandleMovement((IEnemy)collidable);
                }
                collidable.Velocity = oldVelocity;
            }
        }




        public string MessageToReceivers { get; set; }

        public void Receive(List<string> messages)
        {
            active = false;
            sprite.AnimationSpeed = speed;
            foreach (string message in messages)
            {
                if (message.Equals("activate"))
                {
                    active = true;
                    sprite.AnimationSpeed = activatedSpeed;
                }
            }
        }
    }
}
