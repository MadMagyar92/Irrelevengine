using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public abstract class AbstractCharacter : ICollidable, ISender, IReceiver
    {
        public int HealthLevel { get; set; }
        protected int maxHealthLevel;
        public int MeterLevel { get; set; }
        protected int maxMeterLevel;

        public PickupItem CurrentItem { get; set; }
        public ICollectable CurrentPowerup { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }

        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }
        public AbstractCharacterStateManager StateManager { get; set; }

        protected AbstractAnimatedSprite sprite;
        protected CharacterDrawingManager characterDrawingManager;

        public AbstractCharacter()
        {

        }

        public virtual void Receive(List<string> messages)
        {

        }

        public virtual void Update()
        {
            UpdateHitbox();
        }


        #region Should NOT Be Overridden

        private float ItemXPosition()
        {
            if (StateManager.IsFacingRight)
            {
                return Position.X + Size.X + 1;
            }
            else
            {
                return Position.X - CurrentItem.Size.X - 1;
            }
        }
        
        protected void UpdateHitbox()
        {
            CharacterCollisionManager.HandleMovementAndCollisions(this);
        }

        public virtual void Jump()
        {
            float jumpMult = 1;
            if (CurrentItem != null)
            {
                jumpMult = Constants.ENCUMB_JMP_CONST;
            }

            Velocity = new Vector2(Velocity.X, -Constants.CHAR_JMP_CONST * jumpMult);
        }

        public virtual void ChangeHealthLevel(int value)
        {
            if (HealthLevel + value >= maxHealthLevel)
            {
                HealthLevel = maxHealthLevel;
            }
            else if (HealthLevel + value < 0)
            {
                HealthLevel = 0;
            }
            else
            {
                HealthLevel += value;
            }
        }

        public virtual void ChangeMeterLevel(int value)
        {
            if (MeterLevel + value > maxMeterLevel)
            {
                MeterLevel = maxMeterLevel;
            }
            else if (MeterLevel + value < 0)
            {
                MeterLevel = 0;
            }
            else
            {
                MeterLevel += value;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            characterDrawingManager.Draw(spriteBatch, camera);
        }

        public virtual void PickupItem()
        {
            if (CurrentItem == null)
            {
                float newItemX = ItemXPosition();

                List<PickupItem> items = CollisionDetector.GetObjectsAtPosition<PickupItem>(this, new Vector2(newItemX, Position.Y));
                if (items.Count > 0)
                {
                    CurrentItem = items[0];
                    items[0].CollisionType = CollisionType.Liquid;
                    StateManager.IsCarryingObject = true;
                }
            }
        }

        public virtual void DropItem()
        {
            float newItemX = ItemXPosition();

            if (CollisionDetector.IsPositionFree(CurrentItem, new Vector2(newItemX, Position.Y + Size.Y - CurrentItem.Size.Y)))
            {
                CurrentItem.CollisionType = CollisionType.Solid;
                CurrentItem.Position = new Vector2(newItemX, Position.Y);
                CurrentItem.Velocity = new Vector2(0, Velocity.Y);
                StateManager.IsCarryingObject = false;
                CurrentItem = null;
            }
        }

        public virtual void LandOnGround()
        {
            if (Velocity.Y >= 0)
            {
                Velocity = new Vector2(Velocity.X, 0);
                StateManager.HandleLanding();
            }
        }

        public virtual void HitBlockFromBelow()
        {
            Velocity = new Vector2(Velocity.X, Math.Max(0, Velocity.Y));
        }

        public virtual void GoToAir()
        {
            StateManager.GoToAir();
        }

        public virtual void UpdateItemPosition()
        {
            if (CurrentItem != null && CurrentItem is PickupItem)
            {
                CurrentItem.Position = new Vector2(Position.X, Position.Y - CurrentItem.Size.Y);
                ((PickupItem)CurrentItem).FloorCollision();
            }
        }



        public virtual void SetSprite(AbstractAnimatedSprite sprite)
        {
            this.sprite = sprite;
        }

        public virtual AbstractAnimatedSprite GetSprite()
        {
            return sprite;
        }

        #endregion
    }
}
