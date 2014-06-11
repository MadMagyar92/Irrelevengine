using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class CharacterStateManagerImpl : AbstractCharacterStateManager
    {
        private CharacterImpl character;

        public CharacterStateManagerImpl(CharacterImpl player)
        {
            character = player;
            IsCarryingObject = false;
            IsFacingRight = false;
            IsCollidingWithPushableObject = false;
            IsDead = false;
            MovementState = CharacterMovementStates.Idle;
            SpriteState = CharacterSpriteStates.IdleLeft;
        }

        public override void HandleDeath()
        {
            if (IsDead)
            {
                MovementState = CharacterMovementStates.Idle;
                SpriteState = CharacterSpriteStates.Dead;
                character.SetSprite(SpriteFactory.GetCharacterDeadSprite(Constants.CHARACTER_TYPE_1));
                LevelManager.SendEvent(EventFactory.GetDefaultDeathEvent());
            }
        }

        public override void HandleMoveLeftInput()
        {
            if (MovementState == CharacterMovementStates.Idle && !IsFacingRight)
            {
                MovementState = CharacterMovementStates.Walking;
                character.SetSprite(SpriteFactory.GetCharacterWalkingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Walking && IsCollidingWithPushableObject)
            {
                MovementState = CharacterMovementStates.Pushing;
                character.SetSprite(SpriteFactory.GetCharacterPushingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left));
            }
            else if (MovementState == CharacterMovementStates.Idle && IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Walking && IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Pushing && !IsFacingRight)
            {
                MovementState = CharacterMovementStates.Pushing;
            }
            else if (MovementState == CharacterMovementStates.Pushing && IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Jumping)
            {
                MovementState = CharacterMovementStates.Jumping;
                character.SetSprite(SpriteFactory.GetCharacterJumpingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
                IsInAir = true;
            }

            IsFacingRight = false;
        }

        public override void HandleMoveRightInput()
        {
            if (MovementState == CharacterMovementStates.Idle && IsFacingRight)
            {
                MovementState = CharacterMovementStates.Walking;
                character.SetSprite(SpriteFactory.GetCharacterWalkingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Walking && IsCollidingWithPushableObject)
            {
                MovementState = CharacterMovementStates.Pushing;
                character.SetSprite(SpriteFactory.GetCharacterPushingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right));
            }
            else if (MovementState == CharacterMovementStates.Idle && !IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Walking && !IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Pushing && IsFacingRight)
            {
                MovementState = CharacterMovementStates.Pushing;
            }
            else if (MovementState == CharacterMovementStates.Pushing && !IsFacingRight)
            {
                MovementState = CharacterMovementStates.Idle;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, IsCarryingObject));
            }
            else if (MovementState == CharacterMovementStates.Jumping)
            {
                MovementState = CharacterMovementStates.Jumping;
                character.SetSprite(SpriteFactory.GetCharacterJumpingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, IsCarryingObject));
                IsInAir = true;
            }

            IsFacingRight = true;
        }

        public override void HandleJumpInput()
        {
            if (GoToAir())
            {
                character.Jump();
            }
        }
        public override bool GoToAir()
        {
            //return true if we had to switch stuff to be in air
            if ((MovementState == CharacterMovementStates.Idle ||
                        MovementState == CharacterMovementStates.Pushing ||
                        MovementState == CharacterMovementStates.Walking) && !IsInAir)
            {
                MovementState = CharacterMovementStates.Jumping;
                IsInAir = true;

                if (IsFacingRight)
                {
                    character.SetSprite(SpriteFactory.GetCharacterJumpingSprite(Constants.CHARACTER_TYPE_1,  SpriteOrientation.Right, IsCarryingObject));
                }
                else
                {
                    character.SetSprite(SpriteFactory.GetCharacterJumpingSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, IsCarryingObject));
                }
                return true;
            }
            return false;
        }

        public override void HandleLanding()
        {
            IsInAir = false;

            if (MovementState == CharacterMovementStates.Jumping)
            {
                MovementState = CharacterMovementStates.Idle;
                SpriteState = IsFacingRight ? CharacterSpriteStates.IdleRight : CharacterSpriteStates.IdleLeft;
                character.SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, IsFacingRight ? SpriteOrientation.Right : SpriteOrientation.Left, IsCarryingObject));
            }
        }

        public override void HandlePickupInput()
        {
            MovementState = CharacterMovementStates.Idle;
            if (IsCarryingObject)
            {
                character.DropItem();
            }
            else
            {
                character.PickupItemCheck();
            }
        }

        public override void HandleDrinkInput()
        {
            if (character.HealthLevel < 4 && character.MeterLevel > 0 && character.CurrentItem is FishbowlItem)
            {
                character.HealthLevel += 1;
                character.MeterLevel -= 1;
            }

        }
    }
}
