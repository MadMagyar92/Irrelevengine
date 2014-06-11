using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class UIStartMenu
    {
        private bool isInLevelsMenu;
        private bool isFinished;
        private UIButton startGameButton, levelSelectButton, selectLevel0Button, selectLevel1Button;
        private AbstractAnimatedSprite startMenuSprite, levelMenuSprite;
        
        public UIStartMenu()
        {
            this.startGameButton = new UIButton(new Rectangle(61, 105, 362, 32), null);
            this.levelSelectButton = new UIButton(new Rectangle(151, 203, 184, 32), null);
            this.selectLevel0Button = new UIButton(new Rectangle(138, 114, 212, 40), null);
            this.selectLevel1Button = new UIButton(new Rectangle(138, 183, 212, 40), null);
            this.startMenuSprite = SpriteFactory.GetStartMenuSprite();
            this.levelMenuSprite = SpriteFactory.GetLevelMenuSprite();
            this.isInLevelsMenu = false;
            this.isFinished = false;
        }

        public void Update()
        {
            if (!isFinished)
            {
                if (startGameButton.ShouldNotifyReceivers && !isInLevelsMenu)
                {
                    LevelManager.SetStartingLevelIndex(Constants.HUB_LEVEL_INDEX);
                    StartGame();
                    isFinished = true;
                }
                else if (levelSelectButton.ShouldNotifyReceivers && !isInLevelsMenu)
                {
                    SwitchToLevelsMenu();
                }
                else if (selectLevel0Button.ShouldNotifyReceivers && isInLevelsMenu)
                {
                    LevelManager.SetStartingLevelIndex(1);
                    StartGame();
                    isFinished = true;
                }
                else if (selectLevel1Button.ShouldNotifyReceivers && isInLevelsMenu)
                {
                    LevelManager.SetStartingLevelIndex(4);
                    StartGame();
                    isFinished = true;
                }

                startGameButton.Update();
                levelSelectButton.Update();
                selectLevel0Button.Update();
                selectLevel1Button.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (!isFinished)
            {
                if (!isInLevelsMenu)
                {
                    startMenuSprite.Draw(spriteBatch, 0, 0);
                }

                else
                {
                    levelMenuSprite.Draw(spriteBatch, 0, 0);
                }
            }
        }

        private void StartGame()
        {
            LevelManager.Start();
        }

        private void SwitchToLevelsMenu()
        {
            isInLevelsMenu = true;
        }
    }
}
