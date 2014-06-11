using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class TimeStopSprite : AbstractAnimatedSprite
    {
        public TimeStopSprite()
        {
            AnimationSpeed = 0f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 1;
        }
    }
}
