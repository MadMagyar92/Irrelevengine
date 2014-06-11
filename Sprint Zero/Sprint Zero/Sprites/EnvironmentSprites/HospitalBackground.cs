using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class HospitalBackground : AbstractAnimatedSprite
    {

        public HospitalBackground()
        {
            AnimationSpeed = .0f;
            animationIndex = 0f;
            frameCount = 1;
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Color color, Camera camera)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                Texture,
                new Rectangle(x, y, camera.View.Width, camera.View.Height),
                color);
            spriteBatch.End();
        }
    }
}
