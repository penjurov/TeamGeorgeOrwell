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
            this.Attack = 0;
            this.Defence = 0;
            this.Health = 0;
            this.ExpGiven = 0;
        }
    }
}