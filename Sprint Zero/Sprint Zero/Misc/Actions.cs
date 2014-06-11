using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    //Enumeration containing any in-game action that can be bound to input. This would include
    //things such as menu transitions and running, and much more.
    public enum Actions { Quit, MoveLeft, MoveRight, Jump, PickUp, Drink, Reset};
    //When changing these, be sure to add a key mapping!
}
