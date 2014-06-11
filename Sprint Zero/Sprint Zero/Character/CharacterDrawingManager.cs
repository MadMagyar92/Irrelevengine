using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    /*
     * Drawing for the AbstractCharacter was starting to get excessive with blinking and collidable recoloring due to powerup handling. Hence, we've decided
     * that the best route to take for this is to implement a static class with a Draw() method that will draw the collidable for us. This does contribute to
     * the code smell that deals with many classes making up a single class, however, for code extendability purposes, it is safe to argue that this class is
     * necessary for adding additional drawing mechanisms in the future. E.g., more powerups.
     */ 

    public class CharacterDrawingManager
    {
        public bool IsFinished;
        private bool isBlinking = false;
        private int blinkingSequence = 120;
        private int invincibilityStarSequence = 0;
        private AbstractCharacter character;
        private HUD hud;

        public CharacterDrawingManager(AbstractCharacter character)
        {
            this.character = character;
            this.hud = new HUD(character);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (isBlinking)
            {
                if (blinkingSequence % 8 <= 4)
                {
                    DrawCharacter(character, spriteBatch, camera);
                }

                blinkingSequence--;

                if (blinkingSequence <= 0)
                {
                    blinkingSequence = 120;
                    isBlinking = false;
                    IsFinished = true;
                }
            }

            else
            {
                DrawCharacter(character, spriteBatch, camera);
            }
        }

        private void DrawCharacter(AbstractCharacter character, SpriteBatch spriteBatch, Camera camera)
        {
            Color color = GetPowerupColor(character);

            character.Draw(spriteBatch, camera);
            hud.Draw(spriteBatch, camera);
            
        }

        private Color GetPowerupColor(AbstractCharacter character)
        {
            Color color = Color.White;

            if (character.CurrentPowerup is InvincibilityStar)
            {
                if (invincibilityStarSequence < 10)
                {
                    color = Color.Red;
                }
                else if (invincibilityStarSequence >= 10 && invincibilityStarSequence <= 20)
                {
                    color = Color.AntiqueWhite;
                }
                else if (invincibilityStarSequence >= 20)
                {
                    color = Color.Blue;
                }

                if (invincibilityStarSequence > 30)
                {
                    invincibilityStarSequence = 0;
                }

                invincibilityStarSequence++;
            }

            return color;
        }

        public void SetCharacterBlinking(bool isCharacterBlinking)
        {
            isBlinking = isCharacterBlinking;
            blinkingSequence = 120;
            IsFinished = false;
        }
    }
}
