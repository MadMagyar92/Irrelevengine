using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class ConveyorSprite : AbstractAnimatedSprite
    {
        private const float animationToSpeedScale = 1f;

        Texture2D leftTexture, middleTexture, rightTexture;
        public float RotationSpeed { get; set; }
        public SpriteOrientation SpriteOrientation { get; set; }
        private int blockWidth;
        private List<Rectangle> textureRects;

        public ConveyorSprite(int blockWidth, SpriteOrientation spriteOrientation, float rotationSpeed)
        {
            this.SpriteOrientation = spriteOrientation;
            this.RotationSpeed = rotationSpeed;
            this.blockWidth = blockWidth;
            AnimationSpeed = rotationSpeed * animationToSpeedScale;
            animationIndex = 0f;
            frameWidth = 8;
            frameHeight = 8;
            frameCount = 4;
            textureRects = new List<Rectangle>(blockWidth);
        }

        public void SetSpeed(float speed, SpriteOrientation direction)
        {
            SpriteOrientation = direction;
            AnimationSpeed = speed * animationToSpeedScale * (direction == SpriteOrientation.Clockwise ? -1 : 1);
        }

        public void SetLeftTexture(Texture2D texture)
        {
            leftTexture = texture;
        }
        public void SetMiddleTexture(Texture2D texture)
        {
            middleTexture = texture;
        }
        public void SetRightTexture(Texture2D texture)
        {
            rightTexture = texture;
        }
        public void PrepareConveyor()
        {
            if (blockWidth < 1)
            {
                blockWidth = 1; //No empty conveyors
            }

            int i = 0;
            textureRects.Add(new Rectangle(0, 0, frameWidth, frameHeight));
            for (i = 1; i < blockWidth * 2 - 1; i += 2)
            {
                textureRects.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
                textureRects.Add(new Rectangle((i + 1) * frameWidth, 0, frameWidth, frameHeight));
            }
            textureRects.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
        }

        public Vector2 GetSize()
        {
            return new Vector2(textureRects.Count * frameWidth, frameHeight);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            spriteBatch.Draw(//left
                leftTexture, 
                textureRects[0], 
                new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight), 
                Color.White, 
                0, 
                new Vector2(-x,-y), 
                SpriteEffects.None, 
                0);

            for(int i = 1; i < textureRects.Count - 1; i++)
            {
                Rectangle rect = textureRects[i];
                spriteBatch.Draw(//middle
                    middleTexture, 
                    rect, 
                    new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight), 
                    Color.White, 
                    0,
                    new Vector2(-x, -y), 
                    SpriteEffects.None, 
                    0);
            }

            spriteBatch.Draw(//right
                rightTexture, 
                textureRects[textureRects.Count-1], 
                new Rectangle(frameWidth * (int)animationIndex, 0, frameWidth, frameHeight), 
                Color.White, 
                0,
                new Vector2(-x, -y), 
                SpriteEffects.None, 
                0);

            spriteBatch.End();
        }
    }
}
