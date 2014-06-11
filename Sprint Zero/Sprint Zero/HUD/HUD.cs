using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Completely_Irrelevant
{
    public class HUD
    {
        private AbstractAnimatedSprite waterMeter0p;
        private AbstractAnimatedSprite waterMeter25p;
        private AbstractAnimatedSprite waterMeter50p;
        private AbstractAnimatedSprite waterMeter75p;
        private AbstractAnimatedSprite waterMeter100p;
        private AbstractAnimatedSprite powerUpViewer;
        private AbstractAnimatedSprite sunscreen;
        private AbstractAnimatedSprite bubbleShield;
        private AbstractAnimatedSprite invincibilityStar;
        private AbstractCharacter character;
        private AbstractAnimatedSprite[] heartFull;
        private AbstractAnimatedSprite[] heartEmpty;

        private int maxHealth;
        private int vertOffset;
        private int[] horizOffset;
        private int vertOffsetScore;

        public HUD(AbstractCharacter character)
        {
            this.character = character;
            this.maxHealth = character.HealthLevel;
            this.vertOffset = 2;
            this.vertOffsetScore = 8;
            this.horizOffset = new int[maxHealth + 3];
            this.heartEmpty = new AbstractAnimatedSprite[maxHealth];
            this.heartFull = new AbstractAnimatedSprite[maxHealth];

            for (int i = 0; i < maxHealth; i++)
            {
                this.horizOffset[i] = 2 + 18 * i;
                this.heartEmpty[i] = SpriteFactory.GetHeartEmptySprite();
                this.heartFull[i] = SpriteFactory.GetHeartFullSprite();
            }

            this.horizOffset[maxHealth] = this.horizOffset[maxHealth - 1] + 34;
            this.horizOffset[maxHealth + 1] = this.horizOffset[maxHealth] + 34;
            this.horizOffset[maxHealth + 2] = this.horizOffset[maxHealth + 1] + 200;

            this.powerUpViewer = SpriteFactory.GetPowerUpViewerSprite();

            this.waterMeter0p = SpriteFactory.GetWaterMeter0pSprite();
            this.waterMeter25p = SpriteFactory.GetWaterMeter25pSprite();
            this.waterMeter50p = SpriteFactory.GetWaterMeter50pSprite();
            this.waterMeter75p = SpriteFactory.GetWaterMeter75pSprite();
            this.waterMeter100p = SpriteFactory.GetWaterMeter100pSprite();

            this.sunscreen = SpriteFactory.GetSunscreenSprite();
            this.invincibilityStar = SpriteFactory.GetInvincibilityStarSprite();
            this.bubbleShield = SpriteFactory.GetBubbleShieldSprite();
        }

        public void Draw(SpriteBatch spritebatch, Camera camera)
        {
            DrawHearts(spritebatch);
            DrawWaterMeter(spritebatch);
            DrawPowerUp(spritebatch);
            DrawScore(spritebatch);
        }

        public void DrawHearts(SpriteBatch spritebatch)
        {
            int health = character.HealthLevel;

            for (int i = 0; i < health; i++)
            {
                heartFull[i].Draw(spritebatch, horizOffset[i], vertOffset);
            }

            for (int i = health; i < maxHealth; i++)
            {
                heartEmpty[i].Draw(spritebatch, horizOffset[i], vertOffset);
            }
        }

        public void DrawWaterMeter(SpriteBatch spritebatch)
        {
            int waterLevel = character.MeterLevel;

            if (waterLevel == 4)
            {
                waterMeter100p.Draw(spritebatch, horizOffset[maxHealth], vertOffset);
            }

            else if (waterLevel == 3)
            {
                waterMeter75p.Draw(spritebatch, horizOffset[maxHealth], vertOffset);
            }

            else if (waterLevel == 2)
            {
                waterMeter50p.Draw(spritebatch, horizOffset[maxHealth], vertOffset);
            }

            else if (waterLevel == 1)
            {
                waterMeter25p.Draw(spritebatch, horizOffset[maxHealth], vertOffset);
            }

            else if (waterLevel == 0)
            {
                waterMeter0p.Draw(spritebatch, horizOffset[maxHealth], vertOffset);
            }
        }

        public void DrawPowerUp(SpriteBatch spritebatch)
        {
            ICollectable currentPowerUp = character.CurrentPowerup;

            if (currentPowerUp == null)
            {

            }

            else if (currentPowerUp is InvincibilityStar)
            {
                invincibilityStar.Draw(spritebatch, horizOffset[maxHealth + 1], vertOffset);
            }

            powerUpViewer.Draw(spritebatch, horizOffset[maxHealth + 1], vertOffset);
        }

        public void DrawScore(SpriteBatch spritebatch)
        {
            ScoreManager.Draw(spritebatch, horizOffset[maxHealth + 2], vertOffsetScore);
        }
    }
}
