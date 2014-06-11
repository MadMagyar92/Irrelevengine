using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class HealthBottle : ICollectable
    {
        public bool HasBeenConsumed { get; set; }
        public bool EffectHasFinished { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }

        private AbstractAnimatedSprite sprite;
        private AbstractCharacter character;
        private int healthToRestore;

        public HealthBottle(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, int healthToRestore, string message)
        {
            this.HasBeenConsumed = false;
            this.EffectHasFinished = false;
            this.ShouldNotifyReceivers = false;
            this.MessageToReceivers = message;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Velocity = new Vector2(0, 0);
            this.CollisionType = collisionType;
            this.sprite = sprite;
            this.healthToRestore = healthToRestore;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!HasBeenConsumed)
            {
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
            }
        }

        public void Consume()
        {
            HasBeenConsumed = true;
            character.ChangeHealthLevel(healthToRestore);
        }

        public void Destroy()
        {
            EffectHasFinished = true;
        }

        public void SetCharacter(AbstractCharacter player)
        {
            character = player;
        }

        public void Receive(List<string> messages)
        {

        }
    }
}
