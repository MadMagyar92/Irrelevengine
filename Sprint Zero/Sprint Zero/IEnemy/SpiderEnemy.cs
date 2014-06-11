using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class SpiderEnemy : IEnemy
    {
        public int Power { get; set; }
        public bool IsAlive { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public CollisionType CollisionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public string MessageToReceivers { get; set; }

        private int jumpFrequency;
        private int jumpCurrent;
        private bool isJumping;
        private SpriteOrientation spriteOrientation;
        private AbstractAnimatedSprite sprite;

        public SpiderEnemy(Rectangle position, int power, int speed, int jumpFrequency, SpriteOrientation spriteOrientation, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Power = power;
            this.Velocity = new Vector2(0, (spriteOrientation == SpriteOrientation.Up ? -1 : 1 ) * speed);
            this.isJumping = false;
            this.jumpFrequency = jumpFrequency;
            this.jumpCurrent = 0;
            this.IsAlive = true;
            this.ShouldNotifyReceivers = false;
            this.spriteOrientation = spriteOrientation;
            this.sprite = SpriteFactory.GetSpiderSprite(this.spriteOrientation);
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
        }

        public void Update()
        {
            if (!IsAlive)
            {
                return;//Dead
            }
            if (isJumping)
            {
                IEnemyCollisionManager.HandleMovement(this);
            }
            else
            {
                jumpCurrent = (jumpCurrent + 1) % jumpFrequency;
                if (jumpCurrent == 0)
                {
                    isJumping = true;
                }
            }

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
            if (spriteOrientation == SpriteOrientation.Up)
            {
                spriteOrientation = SpriteOrientation.Down;
            }
            else if (spriteOrientation == SpriteOrientation.Down)
            {
                spriteOrientation = SpriteOrientation.Up;
            }
            Velocity = new Vector2(0, -Velocity.Y);

            isJumping = false;
            sprite = SpriteFactory.GetSpiderSprite(this.spriteOrientation);
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
