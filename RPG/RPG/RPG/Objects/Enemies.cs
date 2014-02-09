namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Enemies : Characters
    {
        public Enemies(Vector2 pos, float movSpeed) : base(pos, movSpeed)
        {
        }

        public Skills Skill { get; set; }

        public float ExpGiven { get; set; }
    }
}