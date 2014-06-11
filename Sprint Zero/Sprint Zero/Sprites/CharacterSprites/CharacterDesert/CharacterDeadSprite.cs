using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class CharacterDeadSprite : AbstractAnimatedSprite
    {
        public CharacterDeadSprite()
        {
            AnimationSpeed = .1f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 16;
            frameCount = 1;
        }
    }
}
