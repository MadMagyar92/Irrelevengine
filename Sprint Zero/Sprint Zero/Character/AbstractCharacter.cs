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

        public IItem CurrentItem { get; set; }
        public ICollectable CurrentPowerup { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }


        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        protected AbstractAnimatedSprite sprite;
        public AbstractCharacterStateManager StateManager;
        protected CharacterDrawingManager characterDrawingManager;

        public AbstractCharacter()
        {
            /**
             * TODO: INSERT CONSTRUCTOR LOGIC HERE
             * */
        }

        public virtual void Update()
        {
            /**
             * TODO: INSERT UPDATE LOGIC HERE
             * */
        }

        public virtual void ChangeHealthLevel(int value)
        {
            if (HealthLevel + value >= maxHealthLevel)
            {
                HealthLevel = maxHealthLevel;
                /**
                 * TODO: INSERT MAXED HEALTH LOGIC HERE
                 * */
            }
            else if (HealthLevel + value < 0)
            {
                HealthLevel = 0;
                /**
                 * TODO: INSERT DEATH LOGIC HERE
                 * */
            }
            else
            {
                HealthLevel += value;
                /**
                 * TODO: INSERT HEALTH INCREASE LOGIC HERE
                 * */
            }
        }

        public virtual void ChangeMeterLevel(int value)
        {
            if (MeterLevel + value > maxMeterLevel)
            {
                MeterLevel = maxMeterLevel;
                /**
                 * TODO: INSERT MAXED HEALTH LOGIC HERE
                 * */
            }
            else if (MeterLevel + value < 0)
            {
                MeterLevel = 0;
                /**
                 * TODO: INSERT DEATH LOGIC HERE
                 * */
            }
            else
            {
                MeterLevel += value;
                /**
                 * TODO: INSERT HEALTH INCREASE LOGIC HERE
                 * */
            }
        }

        public virtual void SetSprite(AbstractAnimatedSprite sprite)
        {
            this.sprite = sprite;
        }

        public virtual void Receive(List<string> messages)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            /**
             * TODO: INSERT DRAW LOGIC HERE
             * */
            characterDrawingManager.Draw(spriteBatch, camera);
        }

        public virtual void Jump()
        {
            /**
             * TODO: INSERT JUMP LOGIC HERE
             * */
        }

        public virtual void PickupItemCheck()
        {
            float jumpMult = 1;
            if (CurrentItem != null)
            {
                jumpMult = Constants.ENCUMB_JMP_CONST;
            }

            /**
             * TODO: INSERT JUMP EFFECTS HERE
             * */

            Velocity = new Vector2(Velocity.X, -Constants.CHAR_JMP_CONST * jumpMult);
        }

        public virtual void PickupItem(IItem item)
        {

        }

        public virtual void DropItem()
        {

        }

        public virtual void LandOnGround()
        {

        }
        public virtual void HitBlockFromBelow()
        {

        }

        public virtual void GoToAir()
        {

        }

        public virtual void UpdateItemPosition()
        {
            if (CurrentItem != null && CurrentItem is FishbowlItem)
            {
                CurrentItem.Position = new Vector2(Position.X, Position.Y - CurrentItem.Size.Y);
                ((FishbowlItem)CurrentItem).FloorCollision();
            }
        }
    }
}
