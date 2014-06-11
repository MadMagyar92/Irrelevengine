using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public static class LevelManager
    {
        public static bool Finished = false;

        private static int levelIndex;
        private static bool levelHasStarted = false;
        private static Level currentLevel;
        private static AbstractCharacter character;
        private static List<IController> controllers;

        /*
         * Note, the functionality of figuring out which level to go to next is simply looking at the door you touched.
         * Current Level Setup:
         * Level 1:
         *  - Level1A.xml (Initial Level with access to Secret Level)
         * Level 2:
         *  - Level1B.xml (Secret Level) for Level 1
         * Level 3:
         *  - Level1C.xml (Level 1 without access to Secret Level)
         * Level 4:
         *  - If it existed, it would technically be the second level
         */

        public static void Setup(List<IController> controllersList)
        {
            controllers = controllersList;
        }

        public static void SetStartingLevelIndex(int startingLevelIndex)
        {
            levelIndex = startingLevelIndex;
        }

        public static void Start()
        {
            currentLevel = LevelFactory.GetLevel(controllers, levelIndex);
            character = (AbstractCharacter)currentLevel.characterList[0];
            levelHasStarted = true;
        }

        public static void Update()
        {
            if (levelHasStarted)
            {
                if (currentLevel != null && !currentLevel.IsCompleted && !currentLevel.IsFailed)
                {
                    currentLevel.Update();
                }

                if (currentLevel.IsCompleted)
                {
                    SwitchToLevel(currentLevel.NextLevel);
                }

                if (currentLevel.IsFailed)
                {
                    ResetLevel();
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (currentLevel != null && levelHasStarted)
            {
                currentLevel.Draw(spriteBatch);
            }
        }

        public static void SwitchToLevel(int index)
        {
            levelIndex = index;
            currentLevel.StopMusic();
            GetLevel();
        }

        public static void ResetLevel()
        {
            currentLevel.StopMusic();
            GetLevel();
        }

        private static void GetLevel()
        {
            currentLevel.StopMusic();

            if (levelIndex > Constants.MAX_NUM_OF_LEVELS)
            {
                Finished = true;
            }
            else
            {
                currentLevel = LevelFactory.GetLevel(controllers, levelIndex);
                character = (AbstractCharacter)currentLevel.characterList[0];
            }
        }

        public static void SendEvent(AbstractEvent abstractEvent)
        {
            if (currentLevel.IsEventActivated == false)
            {
                currentLevel.RegisterEvent(abstractEvent);
            }
        }
    }
}
