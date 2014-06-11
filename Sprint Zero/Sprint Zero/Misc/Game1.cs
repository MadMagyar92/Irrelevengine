using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 * Team Completely Irrelevant:
 * Dennis Wolfe(.553)
 * Jeffrey Tornwall(.3)
 * Timothy Ryan(.1163)
 * Joseph Bota(.3)
 * 
 * Current Sprint: 6 (Initial Implementation)
 */

namespace Completely_Irrelevant
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        IController keyboard;
        IController gamepadcontroller;
        List<IController> controllers;
        UIStartMenu startMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            SpriteFactory.SetContentManager(Content);
            SoundFactory.SetContentManager(Content);
            LevelFactory.Setup();
            ClockFactory.Setup();
            EventFactory.Setup();
            ScoreManager.Setup();
            keyboard = new KeyboardController();
            gamepadcontroller = new GamePadController();
            controllers = new List<IController>();

            if (gamepadcontroller.IsConnected())
            {
                controllers = new List<IController>(){ gamepadcontroller };
            }
            else
            {
                controllers = new List<IController>() { keyboard };
            }


            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LevelManager.SetStartingLevelIndex(1);
            LevelManager.Setup(controllers);
            startMenu = new UIStartMenu();

            ScoreManager.Setup();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            ClockFactory.Update(gameTime);

            foreach (IController c in controllers)
            {

                if (c.IsActionActive(Actions.Quit))
                {
                    this.Exit();
                }
                else if (c.IsActionActive(Actions.Reset))
                {
                    LevelManager.ResetLevel();
                }

                c.Update();

                if (LevelManager.Finished == true)
                {
                    GameFinished();
                }
                else
                {
                    startMenu.Update();
                    LevelManager.Update();
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            startMenu.Draw(spriteBatch, null);
            LevelManager.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        protected void GameFinished()
        {
            System.Threading.Thread.Sleep(3000);
            this.Exit();
        }
    }
}
