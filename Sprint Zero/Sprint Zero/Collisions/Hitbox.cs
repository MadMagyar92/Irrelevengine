using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public class Hitbox : ICollidable
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Velocity { get; set; }
        public CollisionType CollisionType { get; set; }

        public Hitbox(Vector2 position, Vector2 size, Vector2 velocity, CollisionType type)
        {
            this.Position = position;
            this.Size = size;
            this.Velocity = velocity;       //D
            this.CollisionType = type;       //a
        }                                     //r
    }                                          //k
}
                                                 //S
                                                  //o
                                                   //u
                                                    //l
                                                     //s