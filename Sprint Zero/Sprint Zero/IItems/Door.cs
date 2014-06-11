using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class Door : IItem
    {
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public bool ShouldSwitchToNextLevel { get; private set; }
        public int NextLevel { get; private set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;
        private bool isWaitingForActivation;

        public Door(int nextLevel, bool isWaitingForActivation, AbstractAnimatedSprite sprite, Rectangle position, CollisionType collisionType, string message)
        {
            this.ShouldSwitchToNextLevel = false;
            this.ShouldNotifyReceivers = false;
            this.sprite = sprite;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.CollisionType = collisionType;
            this.NextLevel = nextLevel;
            this.isWaitingForActivation = isWaitingForActivation;
            this.MessageToReceivers = message;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!isWaitingForActivation)
            {
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
            }
        }

        public bool IsItemPickable() { return false; }

        public bool IsItemInteractable() { return true; }

        public bool IsItemPushable() { return false; }

        public void Receive(List<string> messages)
        {
            string s = messages[0];

            if (!s.Equals("deactivate"))
            {
                isWaitingForActivation = false;
            }
        }

        public void HandleCollisionWithCharacter()
        {
            if (!isWaitingForActivation)
            {
                ShouldSwitchToNextLevel = true;
            }
        }
    }
}
