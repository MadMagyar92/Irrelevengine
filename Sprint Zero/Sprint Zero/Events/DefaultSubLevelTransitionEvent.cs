using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class DefaultSubLevelTransitionEvent : AbstractEvent
    {
        private int[] timeIds;
        private String transitionMessage;
        private SpriteFontContainer transitionFont;
        private Vector2 transitionMessagePosition = new Vector2(0, 0);

        public DefaultSubLevelTransitionEvent(TimeSpan timeToTextOverlay, TimeSpan timeToLevelChange, String transitionMessage, SpriteFontContainer transitionFont)
        {
            this.timeIds = new int[2];
            this.timeIds[0] = ClockFactory.RegisterTimer(timeToTextOverlay);
            this.timeIds[1] = ClockFactory.RegisterTimer(timeToLevelChange);
            this.transitionMessage = transitionMessage;
            this.transitionFont = transitionFont;
            this.transitionMessagePosition = new Vector2();

            ClockFactory.StartTimer(timeIds[0]);
        }

        public override void Update()
        {
            if (ClockFactory.IsTimerFinished(timeIds[1]))
            {
                this.level.IsCompleted = true;
                this.IsFinished = true;
            }

            else if (ClockFactory.IsTimerFinished(timeIds[0]))
            {
                float x = camera.Transform.M11 + camera.View.Width / 2f - transitionFont.SpriteFont.MeasureString(transitionMessage).X / 2f;
                float y = camera.Transform.M22 + camera.View.Height / 3f;
                this.transitionMessagePosition = new Vector2(x, y);
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
                spriteBatch.DrawString(transitionFont.SpriteFont, transitionMessage, transitionMessagePosition + new Vector2(0, -1), Color.Black);
                spriteBatch.DrawString(transitionFont.SpriteFont, transitionMessage, transitionMessagePosition + new Vector2(-1, 0), Color.Black);
                spriteBatch.DrawString(transitionFont.SpriteFont, transitionMessage, transitionMessagePosition + new Vector2(0, 1), Color.Black);
                spriteBatch.DrawString(transitionFont.SpriteFont, transitionMessage, transitionMessagePosition + new Vector2(1, 0), Color.Black);
                spriteBatch.DrawString(transitionFont.SpriteFont, transitionMessage, transitionMessagePosition, transitionFont.Color);
                spriteBatch.End();
            }
        }

    }
}

