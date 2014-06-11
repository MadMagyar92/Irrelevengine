using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class ButtonItem : IItem
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;
        private Hitbox pressHitbox;

        public ButtonItem(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.sprite = sprite;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.pressHitbox = new Hitbox(Position, Size, new Vector2(0, 0), CollisionType.Solid);
            this.MessageToReceivers = message;
        }

        public bool IsItemPickable() { return false; }

        public bool IsItemInteractable() { return true; }

        public bool IsItemPushable() { return false; }

        public void Update()
        {
            ShouldNotifyReceivers = false;

            sprite.Update();
            if (!CollisionDetector.IsPositionFree(pressHitbox, Position))
            {
                Press();
            }
            else
            {
                Depress();
            }
        }


        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        //Response Methods
        public void Press()
        {
            MessageToReceivers = "activate";
            ShouldNotifyReceivers = true;

            if (sprite is ButtonSprite && !((ButtonSprite)sprite).IsPressed())
            {
                ((ButtonSprite)sprite).SetPressed();
            }
        }

        public void Depress()
        {
            if (sprite is ButtonSprite && ((ButtonSprite)sprite).IsPressed())
            {
                ((ButtonSprite)sprite).SetUnpressed();
            }
        }

        public void Receive(List<string> messages)
        {

        }
    }
}
