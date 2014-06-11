using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class GolemEnemy : IEnemy
    {
        public int Power { get; set; }
        public bool IsAlive { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;

        public GolemEnemy(Rectangle position, Vector2 velocity, AbstractAnimatedSprite sprite, int power, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Velocity = velocity;
            this.Power = power;
            this.CollisionType = collisionType;
            this.IsAlive = true;
            this.sprite = sprite;
            this.MessageToReceivers = message;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        public void Receive(List<string> messages)
        {

        }

        public void Kill()
        {
            IsAlive = false;
        }
    }
}
