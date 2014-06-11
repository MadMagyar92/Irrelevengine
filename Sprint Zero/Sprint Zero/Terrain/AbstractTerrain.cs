using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Completely_Irrelevant
{
    public abstract class AbstractTerrain : ICollidable, ISender, IReceiver
    {
        public bool ShouldNotifyReceivers { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }
        public string MessageToReceivers { get; set; }

        private AbstractAnimatedSprite sprite;

        protected AbstractTerrain(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.sprite = sprite;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
        }

        public virtual void Update()
        {
            sprite.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            sprite.Draw(spriteBatch, (int)Position.X, (int)Position.Y, camera);
        }

        public virtual void Receive(List<string> messages)
        {

        }
    }
}
