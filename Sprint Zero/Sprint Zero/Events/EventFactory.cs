using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public static class EventFactory
    {
        private static Dictionary<int, AbstractEvent> registeredEvents;

        public static void Setup()
        {
            registeredEvents = new Dictionary<int, AbstractEvent>();
        }

        public static AbstractEvent GetDefaultDeathEvent()
        {
            SpriteFontContainer spriteFontContainer = new SpriteFontContainer(SpriteFactory.GetLightSpriteFont(), Color.Red);
            String deathMessage = "You are Dead";
            AbstractEvent abstractEvent = new DefaultDeathEvent(new TimeSpan(0, 0, 0, 3), new TimeSpan(0, 0, 0, 5), deathMessage, spriteFontContainer);
            return abstractEvent;
        }

        public static AbstractEvent GetDefaultLevelCompleteEvent()
        {
            SpriteFontContainer completeFontContainer = new SpriteFontContainer(SpriteFactory.GetLightSpriteFont(), Color.Azure);
            String completeMessage = "Level Complete!";
            SpriteFontContainer scoreFontContainer = new SpriteFontContainer(SpriteFactory.GetSoulsSpriteFont(), Color.Azure);
            String scoreMessage = "Your score is " + ScoreManager.GetScore();
            AbstractEvent abstractEvent = new DefaultLevelCompleteEvent(new TimeSpan(0, 0, 0, 0), new TimeSpan(0, 0, 0, 3), new TimeSpan(0, 0, 0, 5),
                completeMessage, scoreMessage, completeFontContainer, scoreFontContainer);
            return abstractEvent;
        }

        public static AbstractEvent GetDefaultSubLevelTransitionEvent()
        {
            SpriteFontContainer completeFontContainer = new SpriteFontContainer(SpriteFactory.GetLightSpriteFont(), Color.Orange);
            String completeMessage = "Onward!";
            AbstractEvent abstractEvent = new DefaultSubLevelTransitionEvent(new TimeSpan(0, 0, 0, 0), new TimeSpan(0, 0, 0, 3),
                completeMessage, completeFontContainer);
            return abstractEvent;
        }
    }
}
