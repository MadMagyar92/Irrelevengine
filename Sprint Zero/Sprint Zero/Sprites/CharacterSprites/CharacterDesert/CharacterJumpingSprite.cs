using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class CharacterJumpingSprite : AbstractAnimatedSprite
    {
        private SpriteOrientation spriteOrientation;


        public CharacterJumpingSprite(SpriteOrientation spriteOrientation)
        {
            this.spriteOrientation = spriteOrientation;
            AnimationSpeed = .1f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 1;
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Color color, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            spriteBatch.Draw(
            Texture,
            new Rectangle(x, y, frameWidth, frameHeight),
            new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight),
            color, 0, new Vector2(0, 0),
            (spriteOrientation == SpriteOrientation.Right ?
            SpriteEffects.None : SpriteEffects.FlipHorizontally), 0);

            spriteBatch.End();
        }
    }
}
