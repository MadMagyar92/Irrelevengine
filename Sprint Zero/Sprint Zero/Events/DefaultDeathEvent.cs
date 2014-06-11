using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class DefaultDeathEvent : AbstractEvent
    {
        private int[] timeIds;
        private String deathMessage;
        private SpriteFontContainer deathFont;
        private Vector2 deathMessagePosition = new Vector2(0, 0);

        public DefaultDeathEvent(TimeSpan timeToTextOverlay, TimeSpan timeToLevelReset,
            String deathMessage, SpriteFontContainer deathFont)
        {
            this.timeIds = new int[2];
            this.timeIds[0] = ClockFactory.RegisterTimer(timeToTextOverlay);
            this.timeIds[1] = ClockFactory.RegisterTimer(timeToLevelReset);
            this.deathMessage = deathMessage;
            this.deathFont = deathFont;
            this.deathMessagePosition = new Vector2();

            ClockFactory.StartTimer(timeIds[0]);
        }

        public override void Update()
        {
            if (ClockFactory.IsTimerFinished(timeIds[1]))
            {
                this.level.IsFailed = true;
                this.IsFinished = true;
            }

            else if(ClockFactory.IsTimerFinished(timeIds[0]))
            {
                float x = camera.Transform.M11 + camera.View.Width / 2f - deathFont.SpriteFont.MeasureString(deathMessage).X / 2f;
                float y = camera.Transform.M22 + camera.View.Height / 2f;
                this.deathMessagePosition = new Vector2(x, y);
                ClockFactory.StartTimer(timeIds[1]);
            }

            foreach (AbstractCharacter character in characterList)
            {
                character.Update();
            }

            foreach (IEnemy enemy in enemyList)
            {
                enemy.Update();
            }

            foreach (AbstractTerrain terrain in terrainList)
            {
                terrain.Update();
            }

            foreach (ItemSet itemSet in itemSetList)
            {
                itemSet.Update();
            }

            foreach (ICollectable collectable in collectableList)
            {
                collectable.Update();
            }

            foreach (IItem item in itemList)
            {
                item.Update();
            }

            camera.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            if (ClockFactory.IsTimerFinished(timeIds[0]))
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(deathFont.SpriteFont, deathMessage, deathMessagePosition + new Vector2(0, -1), Color.Black);
                spriteBatch.DrawString(deathFont.SpriteFont, deathMessage, deathMessagePosition + new Vector2(-1, 0), Color.Black);
                spriteBatch.DrawString(deathFont.SpriteFont, deathMessage, deathMessagePosition + new Vector2(0, 1), Color.Black);
                spriteBatch.DrawString(deathFont.SpriteFont, deathMessage, deathMessagePosition + new Vector2(1, 0), Color.Black);
                spriteBatch.DrawString(deathFont.SpriteFont, deathMessage, deathMessagePosition, deathFont.Color);
                spriteBatch.End();
            }
        }

    }
}
