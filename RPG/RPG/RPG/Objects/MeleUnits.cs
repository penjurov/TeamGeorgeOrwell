namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Screens;

    public class MeleUnits : Units
    {
        public MeleUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            //To add stats: health, attack, defence, skills, experience to give.
        }

        public float ExpGiven { get; set; }
        
    }
}