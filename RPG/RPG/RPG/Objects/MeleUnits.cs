namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;
    using Microsoft.Xna.Framework.Input;

    public class MeleUnits : Units
    {
        
        public MeleUnits(Vector2 pos, float speed) : base(pos,speed)
        {
            //To add stats to the unit.

        }

        public float ExpGiven { get; set; }          
    }
}