using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public interface IItem : ICollidable, ISender, IReceiver
    {
        bool IsItemPickable();
        bool IsItemInteractable();
        bool IsItemPushable();
    }
}
