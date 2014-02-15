namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class MeleUnits : Units, IMovable
    {
        
        public MeleUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            this.Health = 260;
            this.Defence = 40;
            this.Attack = 70;
            this.ExpGiven = 230;
            this.Alive = true;
        }        
    }
}