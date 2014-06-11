using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class Teleporter : IItem
    {
        public bool ShouldNotifyReceivers { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;
        private Hitbox hitbox;
        private Vector2 destination;
        private bool isWaitingForActivation;

        public Teleporter(AbstractAnimatedSprite sprite, Rectangle position, CollisionType collisionType, Vector2 destination, bool isWaitingForActivation, string message)
        {
            this.sprite = sprite;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Velocity = new Vector2(0, 0);
            this.CollisionType = collisionType;
            this.destination = destination;
            this.hitbox = new Hitbox(new Vector2(Position.X + 7, Position.Y + 7), new Vector2(35, 51), new Vector2(0, 0), CollisionType);
            this.MessageToReceivers = message;
            this.isWaitingForActivation = isWaitingForActivation;
        }

        public void Update()
        {
            if (!isWaitingForActivation)
            {
                List<AbstractCharacter> characters = CollisionDetector.GetObjectsAtPosition<AbstractCharacter>(hitbox, hitbox.Position);
                foreach (AbstractCharacter character in characters)
                {
                    character.Position = new Vector2(destination.X, destination.Y - character.Size.Y);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!isWaitingForActivation)
            {
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
            }
        }

        public bool IsItemPickable()
        {
            return false;
        }

        public bool IsItemInteractable()
        {
            return true;
        }

        public bool IsItemPushable()
        {
            return false;
        }

        public void Receive(List<string> messages)
        {
            foreach (string message in messages)
            {
                if (message.Equals("activate"))
                {
                    isWaitingForActivation = false;
                }

                else if (message.Equals("deactivate"))
                {
                    isWaitingForActivation = true;
                }
            }
        }
    }
}
