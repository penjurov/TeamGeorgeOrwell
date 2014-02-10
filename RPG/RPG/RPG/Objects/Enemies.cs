namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Enemies : Characters
    {
        public Enemies(Vector2 pos) : base(pos)
        {
        }

        public float ExpGiven { get; set; }
    }
}