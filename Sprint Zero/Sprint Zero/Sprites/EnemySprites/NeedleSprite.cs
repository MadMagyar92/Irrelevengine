using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class NeedleSprite : AbstractAnimatedSprite
    {
        private SpriteOrientation spriteOrientation;
        private int spriteX, spriteY, spriteWidth, spriteHeight;

        public NeedleSprite(SpriteOrientation spriteOrientation)
        {
            this.spriteOrientation = spriteOrientation;
            AnimationSpeed = .05f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 1;
            spriteX = 3;
            spriteY = 7;
            spriteWidth = 8;
            spriteHeight = 3;
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            spriteBatch.Draw(
            Texture,
            new Rectangle(x + spriteWidth / 2, y + spriteHeight / 2, spriteWidth, spriteHeight),
            new Rectangle(spriteX + frameWidth * (int)animationIndex, spriteY, spriteWidth, spriteHeight),
            Color.White,
            (spriteOrientation==SpriteOrientation.Up ?
            (float)(Math.PI / 2) : 0),
            new Vector2(spriteWidth / 2, spriteHeight / 2),
            (spriteOrientation==SpriteOrientation.Right ? 
            SpriteEffects.None : SpriteEffects.FlipHorizontally), 0);

            spriteBatch.End();
        }
    }
}
