using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class SpikedTerrainSprite : AbstractAnimatedSprite
    {
        private SpriteOrientation orientation;

        public SpikedTerrainSprite(SpriteOrientation orientation)
        {
            this.AnimationSpeed = .1f;
            this.animationIndex = 0f;
            this.frameWidth = 16;
            this.frameHeight = 16;
            this.frameCount = 1;
            this.orientation = orientation;
        }

        /*
        public override void Draw(SpriteBatch spriteBatch, int x, int y, Color color, Camera camera)
        {
            float angles = 0f;

            if (orientation == SpriteOrientation.Left)
            {
                angles = -MathHelper.PiOver2;
            }
            else if (orientation == SpriteOrientation.Right)
            {
                angles = MathHelper.PiOver2;
            }
            else if (orientation == SpriteOrientation.Down)
            {
                angles = -MathHelper.Pi;
            }

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            spriteBatch.Draw(
                Texture,
                new Rectangle(x, y, frameWidth, frameHeight),
                new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight),
                color, angles, new Vector2(frameWidth/2, frameHeight/2), SpriteEffects.None, 0f);
            spriteBatch.End();
        }
         * */
    }
}
