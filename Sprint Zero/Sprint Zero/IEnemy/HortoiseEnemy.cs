using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class HortoiseEnemy : IEnemy
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public int Power { get; set; }
        public bool IsAlive { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        private SpriteOrientation spriteOrientation;
        private AbstractAnimatedSprite sprite;

        public HortoiseEnemy(Rectangle position, int power, int velocity, SpriteOrientation spriteOrientation, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Power = power;
            this.Velocity = new Vector2(velocity, 0);
            this.CollisionType = CollisionType.Liquid;
            this.IsAlive = true;
            this.spriteOrientation = spriteOrientation;
            this.sprite = SpriteFactory.GetHortoiseSprite(this.spriteOrientation);
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
        }

        public void Update() //PRAISE THE SUN
        {
            if (!IsAlive)
            {
                return;//Dead
            }
            IEnemyCollisionManager.HandleMovement(this);

            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!IsAlive)
            {
                return;//Dead
            }
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        //Response Methods
        public void ChangeDirection()
        {
            if (spriteOrientation == SpriteOrientation.Right)
            {
                spriteOrientation = SpriteOrientation.Left;
            }
            else if (spriteOrientation == SpriteOrientation.Left)
            {
                spriteOrientation = SpriteOrientation.Right;
            }
            sprite = SpriteFactory.GetHortoiseSprite(this.spriteOrientation);
            Velocity = new Vector2(-Velocity.X, Velocity.Y);
        }

        public void Kill()
        {
            CollisionDetector.RemoveCollidable(this);
            IsAlive = false;
        }

        public void Receive(List<string> messages)
        {

        }
    }
}
