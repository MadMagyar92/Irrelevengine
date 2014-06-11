using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class ButtonSprite : AbstractAnimatedSprite
    {
        public ButtonSprite()
        {
            AnimationSpeed = 0f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 2;
        }

        public void SetPressed()
        {
            animationIndex = 1;
        }

        public void SetUnpressed()
        {
            animationIndex = 0;
        }

        public bool IsPressed()
        {
            return animationIndex == 1;
        }
    }
}
