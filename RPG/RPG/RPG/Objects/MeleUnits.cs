namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Screens;
    using Interfaces;

    public class MeleUnits : Units , IBoostable
    {
        public MeleUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            this.Attack = 0;
            this.Defence = 0;
            this.Health = 0;
            this.ExpGiven = 0;
        }

        public float ExpGiven { get; set; }       
    }
}