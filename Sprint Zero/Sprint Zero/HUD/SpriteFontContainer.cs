using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class SpriteFontContainer
    {
        public SpriteFont SpriteFont { get; set;}
        public Color Color { get; set; }

        public SpriteFontContainer(SpriteFont spriteFont, Color color)
        {
            this.SpriteFont = spriteFont;
            this.Color = color;
        }
    }
}
