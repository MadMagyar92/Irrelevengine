using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    //Implementation Notes:
    //AnimationSpeed decides frames per update.
    //This implementation is rather generic, and could be used as a foundation for other sprites.
    //It only supports sprite sheets with one row, however.

    public class ExampleSprite : AbstractAnimatedSprite
    {
        public ExampleSprite()
        {
            AnimationSpeed = .1f;
            animationIndex = 0f;
            frameWidth = 16;
            frameHeight = 27;
            frameCount = 3;
        }
    }
}
