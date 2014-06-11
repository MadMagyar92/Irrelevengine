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
    public class Sandstone : AbstractTerrain
    {
        private AbstractAnimatedSprite sprite;

        public Sandstone(Rectangle position, AbstractAnimatedSprite sprite, CollisionType collisionType, string message)
            : base(position, sprite, collisionType, message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.sprite = sprite;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.MessageToReceivers = message;
        }

    }
}
