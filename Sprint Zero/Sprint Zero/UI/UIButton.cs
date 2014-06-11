using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completely_Irrelevant
{
    public class UIButton : ISender
    {
        public bool ShouldNotifyReceivers { get; set; }
        public string MessageToReceivers { get; set; }

        private Rectangle position;
        private AbstractAnimatedSprite sprite;
        private MouseState oldMouse, newMouse;

        public UIButton(Rectangle position, AbstractAnimatedSprite sprite)
        {
            this.position = position;
            this.sprite = sprite;
            this.oldMouse = Mouse.GetState();
            this.newMouse = Mouse.GetState();
            this.ShouldNotifyReceivers = false;
        }

        public void Update()
        {
            ShouldNotifyReceivers = false;

            oldMouse = newMouse;
            newMouse = Mouse.GetState();

            int mouseX = newMouse.X;
            int mouseY = newMouse.Y;

            if (mouseX >= position.X && mouseX <= position.X + position.Width && mouseY >= position.Y && mouseY <= position.Y + position.Height)
            {
                if (oldMouse.LeftButton == ButtonState.Pressed && newMouse.LeftButton == ButtonState.Released)
                {
                    ShouldNotifyReceivers = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (sprite != null)
            {
                sprite.Draw(spriteBatch, position.X, position.Y);
            }
        }
    }
}
