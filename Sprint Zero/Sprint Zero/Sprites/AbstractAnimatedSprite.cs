using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public abstract class AbstractAnimatedSprite
    {
        protected float animationIndex;
        protected int frameWidth, frameHeight, frameCount;

        public Texture2D Texture
        {
            get;
            set;
        }

        public float AnimationSpeed
        {
            get;
            set;
        }

        protected AbstractAnimatedSprite()
        {
            AnimationSpeed = .1f;
            animationIndex = 0f;
            frameWidth = 2;
            frameHeight = 2;
            frameCount = 1;
        }

        public virtual void Update()
        {
            animationIndex += AnimationSpeed;
            if ((int)animationIndex >= frameCount)
            {
                animationIndex -= (float)frameCount;
            }
            else if (animationIndex < 0)
            {
                animationIndex += (float)frameCount;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y, Camera camera)
        {
            Draw(spriteBatch, x, y, Color.White, camera);
        }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y, Color color, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            spriteBatch.Draw(
                Texture,
                new Rectangle(x, y, frameWidth, frameHeight),
                new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight),
                color);
            spriteBatch.End();
        }

        public virtual void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                Texture,
                new Rectangle(x, y, frameWidth, frameHeight),
                new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight),
                Color.White);
            spriteBatch.End();
        }
    }
}
