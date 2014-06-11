using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class WaterBottle : ICollectable
    {
        public bool EffectHasFinished { get; set; }
        public bool HasBeenConsumed { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public CollisionType CollisionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public string MessageToReceivers { get; set; }

        private int amountOfWaterToRestore;
        private AbstractAnimatedSprite sprite;
        private AbstractCharacter character;

        public WaterBottle(AbstractAnimatedSprite sprite, int amountOfWaterToRestore, Rectangle position, CollisionType collisionType, string message)
        {
            this.EffectHasFinished = false;
            this.ShouldNotifyReceivers = false;
            this.amountOfWaterToRestore = amountOfWaterToRestore;
            this.sprite = sprite;
            this.CollisionType = collisionType;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.MessageToReceivers = message;
        }

        public void Consume()
        {
            character.ChangeMeterLevel(amountOfWaterToRestore);
            EffectHasFinished = true;
            HasBeenConsumed = true;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if(!HasBeenConsumed)
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        public void SetCharacter(AbstractCharacter player)
        {
            character = player;
        }

        public void Update()
        {

        }

        public void Destroy()
        {

        }

        public void Receive(List<string> messages)
        {

        }
    }
}
