using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class CactusSprite : AbstractAnimatedSprite
    {
        public CactusSprite()
        {
            AnimationSpeed = .05f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 2;
        }
    }
}
