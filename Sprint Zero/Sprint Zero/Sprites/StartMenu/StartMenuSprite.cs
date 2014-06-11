﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class StartMenuSprite : AbstractAnimatedSprite
    {
        public StartMenuSprite()
        {
            AnimationSpeed = .1f;
            animationIndex = 0f;
            frameWidth = 800;
            frameHeight = 480;
            frameCount = 1;
        }
    }
}
