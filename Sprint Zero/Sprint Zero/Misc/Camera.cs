using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class Camera
    {
        public Rectangle View { get; set; }
        public Matrix Transform { get; set; }

        public int MinY { get; set; }
        public int MaxY { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }

        public int LeftPadding { get; set; }
        public int RightPadding { get; set; }
        public int TopPadding { get; set; }
        public int BottomPadding { get; set; }

        public float Scale { get; set; }

        public ICollidable Focus { get; set; }

        public Camera()
        {

        }

        public Camera(Rectangle View, int MinX, int MinY, int MaxX, int MaxY, int PaddingX, int PaddingY, float Scale, ICollidable Focus)
        {
            this.View = View;
            this.MinX = MinX;
            this.MinY = MinY;
            this.MaxX = MaxX;
            this.MaxY = MaxY;
            this.LeftPadding = this.RightPadding = PaddingX;
            this.TopPadding = this.BottomPadding = PaddingY;
            this.Scale = Scale;
            this.Focus = Focus;
            Transform = new Matrix();
        }

        public void Update()
        {
            if (Focus==null)
            {
                return; //Can't update without a focus.
            }
            int focusTop, focusBottom, focusLeft, focusRight;

            focusTop = (int)Focus.Position.Y;
            focusBottom = (int)Focus.Position.Y + (int)Focus.Size.Y;
            focusRight = (int)Focus.Position.X + (int)Focus.Size.X;
            focusLeft = (int)Focus.Position.X;


            float newX = View.X;
            float newY = View.Y;
            
            //Apply padding
            newX = Math.Min(newX, focusLeft - LeftPadding / Scale);
            newX = Math.Max(newX, focusRight + RightPadding / Scale - View.Width / Scale);
            newY = Math.Min(newY, focusTop - TopPadding / Scale);
            newY = Math.Max(newY, focusBottom + BottomPadding / Scale - View.Height / Scale);

            //Apply Max and Min X and Y
            newX = Math.Max(newX, MinX);
            newX = Math.Min(newX, MaxX - View.Width / Scale);
            newY = Math.Max(newY, MinY);
            newY = Math.Min(newY, MaxY - View.Height / Scale);
            
            View = new Rectangle((int)newX, (int)newY, View.Width, View.Height);

            //Calculate Transform
            Transform = Matrix.Identity *
                Matrix.CreateTranslation(-View.X, -View.Y, 0) *
                Matrix.CreateScale(Scale,Scale,Scale);
        }
    }
}
