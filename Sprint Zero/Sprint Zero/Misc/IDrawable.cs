using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch, Camera camera);
    }
}
