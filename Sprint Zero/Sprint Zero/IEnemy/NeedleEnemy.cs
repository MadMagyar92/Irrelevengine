using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class NeedleEnemy : IEnemy
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
        private int speed;

        public NeedleEnemy(Rectangle position, int power, int speed, int frequency, SpriteOrientation spriteOrientation, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Power = power;
            this.Velocity = new Vector2(0, 0);
            this.speed = speed;
            this.IsAlive = true;
            this.spriteOrientation = spriteOrientation;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
            this.SetOrientation(spriteOrientation);

        }

        public void SetOrientation(SpriteOrientation spriteOrientation)
        {
            this.spriteOrientation = spriteOrientation;
            if (this.spriteOrientation == SpriteOrientation.Left)
            {
                this.Velocity = new Vector2(-speed, 0);
            }
            else if (this.spriteOrientation == SpriteOrientation.Right)
            {
                this.Velocity = new Vector2(speed, 0);
            }
            else if (this.spriteOrientation == SpriteOrientation.Up)
            {
                this.Velocity = new Vector2(0, -speed);
            }
            this.sprite = SpriteFactory.GetNeedleSprite(this.spriteOrientation);
        }

        public void Update()
        {
            IEnemyCollisionManager.HandleMovement(this);
            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (IsAlive == true)
            {
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
            }
        }

        //Response Method
        public void Kill()
        {
            IsAlive = false;
            CollisionDetector.RemoveCollidable(this);
        }

        public void Receive(List<string> messages)
        {

        }
    }
}
