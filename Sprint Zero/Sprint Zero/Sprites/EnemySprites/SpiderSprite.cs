﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class SpiderSprite : AbstractAnimatedSprite
    {
        private SpriteOrientation spriteOrientation;

        public SpiderSprite(SpriteOrientation spriteOrientation)
        {
            this.spriteOrientation = spriteOrientation;
            AnimationSpeed = .05f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 2;
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            spriteBatch.Draw(
            Texture,
            new Rectangle(x, y, frameWidth, frameHeight),
            new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight),
            Color.White, 0, new Vector2(0, 0), 
            (spriteOrientation==SpriteOrientation.Up ? 
            SpriteEffects.None : SpriteEffects.FlipVertically), 0);

            spriteBatch.End();
        }

    }
}
