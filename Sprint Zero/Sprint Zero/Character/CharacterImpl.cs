using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class CharacterImpl : AbstractCharacter
    {
        private List<IController> controllers;
        private Rectangle originalHitbox;
        private bool keyPickupHeld;
        private bool isDehydrationEnabled;
        private int dehydrationDamage;
        private int timer;

        public CharacterImpl(int health, int fishbowlWaterLevel, Rectangle position, List<IController> controllersList, bool isDehydrationEnabled, int dehydrationDamage, int dehydrationTime, CollisionType collisionType, string message)
        {
            this.Position = new Vector2(position.X, position.Y);
            this.Size = new Vector2(position.Width, position.Height);
            this.Velocity = new Vector2();
            this.originalHitbox = position;
            this.HealthLevel = health;
            this.maxHealthLevel = health;
            this.MeterLevel = fishbowlWaterLevel;
            this.controllers = controllersList == null ? new List<IController>() : controllersList;
            this.StateManager = new CharacterStateManagerImpl(this);
            this.CurrentItem = null;
            this.ShouldNotifyReceivers = false;
            this.CollisionType = collisionType;
            this.isDehydrationEnabled = isDehydrationEnabled;
            this.keyPickupHeld = false;
            this.dehydrationDamage = dehydrationDamage;
            this.CurrentPowerup = null;
            this.timer = ClockFactory.RegisterTimer(new TimeSpan(0, 0, dehydrationTime));
            this.MessageToReceivers = message;

            ClockFactory.StartTimer(timer);
        }

        public override void Update()
        {
            if (!StateManager.IsDead)
            {
                UpdatePowerups();
                DehydrateCharacter();
                UpdateStateManager();
            }

            UpdateHitbox();

            if (characterDrawingManager.IsFinished)
            {
                SetCharacterInvincibility(false);
                characterDrawingManager.IsFinished = false;
            }

            sprite.Update();
            StateManager.IsCollidingWithPushableObject = false; //Will be set to true after Update if collidable is colliding with a crate or other pushable object.
        }

        private void UpdateHitbox()
        {
            CharacterCollisionManager.HandleMovementAndCollisions(this);
        }

        

        private void UpdatePowerups()
        {
            if (CurrentPowerup != null && CurrentPowerup.EffectHasFinished)
            {
                CurrentPowerup = null;
            }
        }

        private void SetCharacterInvincibility(bool invincibility)
        {
            if (!(CurrentPowerup is InvincibilityStar))
            {
                StateManager.IsInvincible = invincibility;
            }

        }

        public void DehydrateCharacter()
        {
            if (ClockFactory.IsTimerFinished(timer) && isDehydrationEnabled)
            {
                if (!StateManager.IsImmuneToDehydration)
                {
                    SoundFactory.PlayCharacterHurtSound();

                    HealthLevel -= dehydrationDamage;

                    if (HealthLevel <= 0)
                    {
                        StateManager.IsDead = true;
                        StateManager.HandleDeath();
                    }
                    else
                    {
                        characterDrawingManager.SetCharacterBlinking(true);
                    }
                }

                ClockFactory.ResetTimer(timer);
                ClockFactory.StartTimer(timer);
            }
        }

        public override void SetSprite(AbstractAnimatedSprite newSprite)
        {
            this.sprite = newSprite;
        }

        public override void Receive(List<string> messages)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            characterDrawingManager.Draw(spriteBatch, camera);
        }

        #region Input

        private void UpdateStateManager()
        {
            List<Actions> actions = new List<Actions>();

            bool hasPressedLeft = false, hasPressedRight = false, hasDrank = false;

            foreach (IController controller in controllers)
            {
                if (controller.IsActionActive(Actions.MoveLeft))
                {
                    hasPressedLeft = true;
                    if (!hasPressedRight)
                    {
                        actions.Add(Actions.MoveLeft);
                        CollisionDetector.DoPositionsIntersect(Position, Position, Position, Position);
                    }
                }

                if (controller.IsActionActive(Actions.MoveRight))
                {
                    hasPressedRight = true;
                    if (!hasPressedLeft)
                    {
                        actions.Add(Actions.MoveRight);
                    }
                }
                if (!hasPressedLeft && !hasPressedRight)
                {
                    Velocity = new Vector2(0, Velocity.Y);
                }

                if (controller.IsActionActive(Actions.Jump))
                {
                    actions.Add(Actions.Jump);
                }

                if (controller.IsActionActive(Actions.PickUp))
                {
                    if (keyPickupHeld == false)
                    {
                        actions.Add(Actions.PickUp);
                        keyPickupHeld = true;
                    }
                }
                else
                {
                    keyPickupHeld = false;
                }

                if (controller.IsActionActive(Actions.Drink) && !hasDrank)
                {
                    hasDrank = true;
                    actions.Add(Actions.Drink);
                }
            }

            ProcessActions(actions);
        }

        private void ProcessActions(List<Actions> actionList)
        {
            foreach (Actions action in actionList)
            {
                if (action == Actions.MoveRight)
                {
                    StateManager.HandleMoveRightInput();

                    if (StateManager.IsFacingRight)
                    {
                        MoveRight();
                    }
                }

                else if (action == Actions.MoveLeft)
                {
                    StateManager.HandleMoveLeftInput();

                    if (!StateManager.IsFacingRight)
                    {
                        MoveLeft();
                    }
                }

                else if (action == Actions.Jump)
                {
                    StateManager.HandleJumpInput();
                }

                else if (action == Actions.PickUp)
                {
                    StateManager.HandlePickupInput();
                }

                else if (action == Actions.Drink)
                {
                    StateManager.HandleDrinkInput();
                }
            }

            if (actionList.Count == 0 && !StateManager.IsInAir && !StateManager.IsDead)
            {
                if (Velocity.X == 0 && Velocity.Y == 0 && StateManager.IsFacingRight)
                {
                    StateManager.MovementState = CharacterMovementStates.Idle;
                    SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Right, StateManager.IsCarryingObject));
                }
                else if (Velocity.X == 0 && Velocity.Y == 0 && !StateManager.IsFacingRight)
                {
                    StateManager.MovementState = CharacterMovementStates.Idle;
                    SetSprite(SpriteFactory.GetCharacterIdleSprite(Constants.CHARACTER_TYPE_1, SpriteOrientation.Left, StateManager.IsCarryingObject));
                }
            }
        }

        #endregion

        #region Collision Related Methods

        private void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y + 2);
        }

        private void MoveLeft()
        {
            float speedMult = 1;
            if (CurrentItem != null)
            {
                speedMult = Constants.ENCUMB_SPD_CONST;
            }
            Velocity = new Vector2(-Constants.CHAR_SPD_CONST * speedMult, Velocity.Y);
        }

        private void MoveRight()
        {
            float speedMult = 1;
            if (CurrentItem != null)
            {
                speedMult = Constants.ENCUMB_SPD_CONST;
            }
            Velocity = new Vector2(Constants.CHAR_SPD_CONST * speedMult, Velocity.Y);
        }

        public override void Jump()
        {
            float jumpMult = 1;
            if (CurrentItem != null)
            {
                jumpMult = Constants.ENCUMB_JMP_CONST;
            }
            SoundFactory.PlayCharacterJumpingSound();
            Velocity = new Vector2(Velocity.X, -Constants.CHAR_JMP_CONST * jumpMult);
        }

        public override void PickupItemCheck()
        {
            if (CurrentItem == null)
            {
                float newItemX;
                if (StateManager.IsFacingRight)
                {
                    newItemX = Position.X + Size.X + 1;
                }
                else
                {
                    newItemX = Position.X - 1;
                }
                List<FishbowlItem> items = CollisionDetector.GetObjectsAtPosition<FishbowlItem>(this, new Vector2(newItemX, Position.Y));
                if (items.Count > 0)
                {
                    PickupItem(items[0]);
                }
            }
        }

        public override void PickupItem(IItem item)
        {
            CurrentItem = item;
            item.CollisionType = CollisionType.Liquid;
            StateManager.IsCarryingObject = true;
        }

        public override void DropItem()
        {
            float newItemX;
            if (StateManager.IsFacingRight)
            {
                newItemX = Position.X + Size.X + 1;
            }
            else
            {
                newItemX = Position.X - CurrentItem.Size.X - 1;
            }

            CurrentItem.CollisionType = CollisionType.Solid;
            if (!CollisionDetector.IsPositionFree(CurrentItem, new Vector2(newItemX, Position.Y + Size.Y - CurrentItem.Size.Y)))
            {
                CurrentItem.CollisionType = CollisionType.Liquid;
                return;
            }

            CurrentItem.Position = new Vector2(newItemX, Position.Y);
            CurrentItem.Velocity = new Vector2(0, Velocity.Y);
            StateManager.IsCarryingObject = false;
            CurrentItem = null;
        }

        public override void LandOnGround()
        {
            if (Velocity.Y >= 0)
            {
                Velocity = new Vector2(Velocity.X, 0);
                StateManager.HandleLanding();
            }
        }
        public override void HitBlockFromBelow()
        {
            Velocity = new Vector2(Velocity.X, Math.Max(0, Velocity.Y));
        }

        public override void GoToAir()
        {
            StateManager.GoToAir();
        }

        #endregion
    }
}
