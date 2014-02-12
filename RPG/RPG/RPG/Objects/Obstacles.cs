namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class Obstacles : Obj
    {
        public Obstacles(Vector2 pos) : base(pos)
        {
            this.Position = pos;
        }

        public int Length { get; set; }

        public float Angle { get; set; }
    }
}