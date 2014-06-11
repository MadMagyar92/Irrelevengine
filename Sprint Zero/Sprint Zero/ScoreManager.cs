using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public static class ScoreManager
    {
        private static SpriteFont spriteFont;
        private static Vector2 position;
        private static int score;

        public static void Setup()
        {
            score = 0;
            spriteFont = SpriteFactory.GetScoreSpriteFont();
        }

        public static void IncrementScore(int amount)
        {
            if (score + amount >= 0)
        {
                score += amount;
            }
            else
            {
                score = 0;
            }
        }

        public static void Draw(SpriteBatch spritebatch, float horizPosition, float vertPosition, Camera camera)
        {
            spritebatch.Begin();
            position = new Vector2(horizPosition,vertPosition);
            //Console.WriteLine("x: " + horizPosition + " y: " + vertPosition);
            string scoreString = "Score: " + score.ToString();
            spritebatch.DrawString(spriteFont, scoreString, position, Color.White);
            spritebatch.End();
        }
    }
}
