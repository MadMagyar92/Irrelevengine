using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class FishbowlItem : IItem
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        private AbstractAnimatedSprite sprite;
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        public FishbowlItem(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, string message)
        {
            this.ShouldNotifyReceivers = false;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.sprite = sprite;
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
        }

        public bool IsItemPickable() { return true; }

        public bool IsItemInteractable() { return false; }

        public bool IsItemPushable() { return false; }

        public void Update()
        {
            IItemCollisionManager.HandleMovement(this);
            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        public void FloorCollision()
        {
            Velocity = new Vector2(Velocity.X, 0);
        }

        public void Receive(List<string> messages)
        {

        }
    }
}
