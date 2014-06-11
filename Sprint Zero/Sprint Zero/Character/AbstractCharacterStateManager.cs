using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public abstract class AbstractCharacterStateManager
    {
        public bool IsInvincible { get; set; }
        public bool IsImmuneToDehydration { get; set; }
        public bool IsFacingRight { get; set; }
        public bool IsCarryingObject { get; set; }
        public bool IsInAir { get; set; }
        public bool IsCollidingWithPushableObject { get; set; }
        public bool IsDead { get; set; }
        public bool HasBubbleShield { get; set; }
        public CharacterSpriteStates SpriteState { get; set; }
        public CharacterMovementStates MovementState { get; set; }

        public AbstractCharacterStateManager()
        {

        }

        public virtual void HandleDeath()
        {

        }

        public virtual void HandleMoveLeftInput()
        {

        }

        public virtual void HandleMoveRightInput()
        {

        }

        public virtual void HandleJumpInput()
        {

        }
        public virtual bool GoToAir()
        {
            return false;
        }

        public virtual void HandleLanding()
        {

        }

        public virtual void HandlePickupInput()
        {

        }

        public virtual void HandleDrinkInput()
        {

        }
    }
}
