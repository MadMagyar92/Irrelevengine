using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class InvincibilityStar : ICollectable
    {
        public bool EffectHasFinished { get; set; }
        public bool HasBeenConsumed { get; set; }
        public bool ShouldNotifyReceivers { get; set; }
        public CollisionType CollisionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public string MessageToReceivers { get; set; }

        private int duration;
        private int timer;
        private AbstractAnimatedSprite sprite;
        private AbstractCharacter character;

        public InvincibilityStar(AbstractAnimatedSprite sprite, int duration, Rectangle position, CollisionType collisionType, string message)
        {
            this.timer = ClockFactory.RegisterTimer(new TimeSpan(0, 0, duration));
            this.EffectHasFinished = false;
            this.HasBeenConsumed = false;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.sprite = sprite;
            this.duration = duration;
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.MessageToReceivers = message;
        }

        public void Consume()
        {
            ClockFactory.StartTimer(timer);
            //character.StateManager.IsInvincible = true;
            //character.StateManager.IsImmuneToDehydration = true;
            HasBeenConsumed = true;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if(!HasBeenConsumed)
                sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        public void Update()
        {
            if (!EffectHasFinished && ClockFactory.IsTimerFinished(timer))
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            EffectHasFinished = true;
            //character.StateManager.IsInvincible = false;
            //character.StateManager.IsImmuneToDehydration = false;
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
