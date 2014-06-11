using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public enum CharacterMovementStates { Idle, Walking, Jumping, Pushing }
    public enum CharacterSpriteStates
    {
        IdleRight, IdleLeft, WalkingRight, WalkingLeft, JumpingRight, JumpingLeft, PushingRight, PushingLeft, Dead
    }
}

//Dark Souls