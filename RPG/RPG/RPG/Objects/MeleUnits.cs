namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class MeleUnits : Units, IMovable
    {
        public MeleUnits(Vector2 pos, float speed, bool act, float att, float def, float hp, float exp, bool alive, float range)
            : base(pos, speed, act, range)
        {
            this.Attack = att;
            this.Defence = def;
            this.Health = hp;
            this.ExpGiven = exp;
            this.Alive = alive;
        }
    }
}