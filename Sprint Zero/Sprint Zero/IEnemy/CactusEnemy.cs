using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class CactusEnemy : IEnemy
    {
        private const int needleWidth = 4;
        private const int needleHeight = 4;


        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public int Power { get; set; }
        public bool IsAlive { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;
        private List<NeedleEnemy> needles;
        private Rectangle needleRect;
        private int needleSpeed;
        private int needleFrequency;
        private int timer;

        public CactusEnemy(Rectangle position, int power, int velocity, int frequency, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Power = power;
            this.IsAlive = true;
            this.sprite = SpriteFactory.GetCactusSprite();
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.Velocity = new Vector2(0, 0);
            this.needleSpeed = velocity;
            this.needleFrequency = frequency;
            this.timer = frequency-15;
            this.MessageToReceivers = message;

            needleRect = new Rectangle(
                position.X + position.Width/2 - needleWidth / 2,
                position.Y + position.Height/2 - needleHeight / 2,
                needleWidth,
                needleHeight);
            needles = new List<NeedleEnemy>(6);
        }

        private void SpawnNeedle(SpriteOrientation orientation)
        {
            NeedleEnemy needle = null;
            for (int i = needles.Count - 1; i >= 0; i--) //Grab a dead needle from the list or...
            {
                if (needles[i].IsAlive)
                {
                    continue;
                }
                needle = needles[i];
                break;
            }
            if (needle == null) //make a new one if there were no dead needles to grab
            {
                needle = new NeedleEnemy(needleRect, this.Power, needleSpeed, needleFrequency, orientation, CollisionType.Liquid, MessageToReceivers);
                needles.Add(needle);
            }
            else
            {
                needle.Position = new Vector2(needleRect.X, needleRect.Y);
                needle.SetOrientation(orientation);
                needle.IsAlive = true;
            }
            CollisionDetector.AddCollidable(needle);


        }

        public void Update()
        {
            sprite.Update();

            timer--;
            for (int i = needles.Count - 1; i >= 0; i--)
            {
                if (!needles[i].IsAlive)
                {
                    continue;
                }
                needles[i].Update();
            }
            if (timer == 0)
            {
                timer = needleFrequency;
                SpawnNeedle(SpriteOrientation.Left);
                SpawnNeedle(SpriteOrientation.Up);
                SpawnNeedle(SpriteOrientation.Right);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
            for (int i = needles.Count - 1; i >= 0; i--)
            {
                if (!needles[i].IsAlive)
                {
                    continue;
                }
                needles[i].Draw(spriteBatch, camera);
            }
        }

        //Response Methods
        public void Kill()
        {
            IsAlive = false;
            for (int i = needles.Count - 1; i >= 0; i--)
            {
                if (!needles[i].IsAlive)
                {
                    continue;
                }
                needles[i].Kill();
            }

            for (int i = 0; i < 3; i++)
            {
                needles[i].Kill();
            }

        }

        public void Receive(List<string> messages)
        {

        }
    }
}
