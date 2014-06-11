using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class TeleporterSprite : AbstractAnimatedSprite
    {
        public TeleporterSprite()
        {
            AnimationSpeed = 0f;
            animationIndex = 0f;
            frameWidth = 48;
            frameHeight = 64;
            frameCount = 1;
        }
    }
}
