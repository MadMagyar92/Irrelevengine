using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    class BonfireSprite : AbstractAnimatedSprite
    {
        public bool Lit { get; set; }
        private int LitIndex;

        public BonfireSprite()
        {
            AnimationSpeed = .02f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 2;
            LitIndex = 0;
        }

        public override void Update()
        {
            if (Lit == true)
            {
                LitIndex = 1;
            }
            else
            {
                LitIndex = 0;
            }

            animationIndex += AnimationSpeed;
            if ((int)animationIndex >= frameCount)
            {
                animationIndex -= (float)frameCount;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            spriteBatch.Draw(
            Texture,
            new Rectangle(x, y, frameWidth, frameHeight),
            new Rectangle(frameWidth * (int)animationIndex, frameHeight * LitIndex, frameWidth, frameHeight),
            Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
