using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public interface ICollectable : ICollidable, ISender, IReceiver
    {
        bool HasBeenConsumed { get; set; }

        bool EffectHasFinished { get; set; }

        void Consume();

        void Destroy();

        void SetCharacter(AbstractCharacter character);
    }
}
