using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class DefaultLevelCompleteEvent : AbstractEvent
    {
        private int[] timeIds;
        private String completeMessage, scoreMessage;
        private SpriteFontContainer completeFont, scoreFont;
        private Vector2 completeMessagePosition = new Vector2(0, 0), scoreMessagePosition = new Vector2(0, 0);

        public DefaultLevelCompleteEvent(TimeSpan timeToTextOverlay, TimeSpan timeToScoreOverlay,
            TimeSpan timeToLevelChange, String completeMessage, String scoreMessage, SpriteFontContainer completeFont, SpriteFontContainer scoreFont)
        {
            this.timeIds = new int[3];
            this.timeIds[0] = ClockFactory.RegisterTimer(timeToTextOverlay);
            this.timeIds[1] = ClockFactory.RegisterTimer(timeToScoreOverlay);
            this.timeIds[2] = ClockFactory.RegisterTimer(timeToLevelChange);
            this.completeMessage = completeMessage;
            this.scoreMessage = scoreMessage;
            this.completeFont = completeFont;
            this.scoreFont = scoreFont;
            this.completeMessagePosition = new Vector2();

            ClockFactory.StartTimer(timeIds[0]);
        }

        public override void Update()
        {
            if (ClockFactory.IsTimerFinished(timeIds[2]))
            {
                this.level.IsCompleted = true;
                this.IsFinished = true;
            }

            else if (ClockFactory.IsTimerFinished(timeIds[1]))
            {
                float x = camera.Transform.M11 + camera.View.Width / 2f - scoreFont.SpriteFont.MeasureString(scoreMessage).X / 2f;
                float y = (camera.Transform.M22 + camera.View.Height) * 2f / 3f;
                this.scoreMessagePosition = new Vector2(x, y);
                ClockFactory.StartTimer(timeIds[2]);
            }

            else if (ClockFactory.IsTimerFinished(timeIds[0]))
            {
                float x = camera.Transform.M11 + camera.View.Width / 2f - completeFont.SpriteFont.MeasureString(completeMessage).X / 2f;
                float y = camera.Transform.M22 + camera.View.Height / 3f;
                this.completeMessagePosition = new Vector2(x, y);
                ClockFactory.StartTimer(timeIds[1]);
            }


            foreach (AbstractTerrain terrain in terrainList)
            {
                terrain.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            if (ClockFactory.IsTimerFinished(timeIds[0]))
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(completeFont.SpriteFont, completeMessage, completeMessagePosition + new Vector2(0, -1), Color.Black);
                spriteBatch.DrawString(completeFont.SpriteFont, completeMessage, completeMessagePosition + new Vector2(-1, 0), Color.Black);
                spriteBatch.DrawString(completeFont.SpriteFont, completeMessage, completeMessagePosition + new Vector2(0, 1), Color.Black);
                spriteBatch.DrawString(completeFont.SpriteFont, completeMessage, completeMessagePosition + new Vector2(1, 0), Color.Black);
                spriteBatch.DrawString(completeFont.SpriteFont, completeMessage, completeMessagePosition, completeFont.Color);
                spriteBatch.End();
            }

            if (ClockFactory.IsTimerFinished(timeIds[1]))
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(scoreFont.SpriteFont, scoreMessage, scoreMessagePosition + new Vector2(0, -1), Color.Black);
                spriteBatch.DrawString(scoreFont.SpriteFont, scoreMessage, scoreMessagePosition + new Vector2(-1, 0), Color.Black);
                spriteBatch.DrawString(scoreFont.SpriteFont, scoreMessage, scoreMessagePosition + new Vector2(0, 1), Color.Black);
                spriteBatch.DrawString(scoreFont.SpriteFont, scoreMessage, scoreMessagePosition + new Vector2(1, 0), Color.Black);
                spriteBatch.DrawString(scoreFont.SpriteFont, scoreMessage, scoreMessagePosition, scoreFont.Color);
                spriteBatch.End();
            }
        }

    }
}

