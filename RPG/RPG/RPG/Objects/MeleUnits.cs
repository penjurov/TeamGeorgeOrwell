namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class MeleUnits : Units, IMovable
    {
        private readonly int hitRate = 60;
        private int hitTimer = 0;

        public MeleUnits(Vector2 pos, float speed, bool act, float att, float def, float hp, float exp, bool alive, float range)
            : base(pos, speed, act, range)
        {
            this.Attack = att;
            this.Defence = def;
            this.Health = hp;
            this.ExpGiven = exp;
            this.Alive = alive;
        }

        public override int HitRate
        {
            get
            {
                return this.hitRate;
            }
        }

        public override int HitTimer
        {
            get
            {
                return this.hitTimer;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Units hit timer cannot be a negative number!", value);
                }
                this.hitTimer = value;
            }
        }
    }
}