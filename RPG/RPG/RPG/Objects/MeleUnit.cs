namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class MeleUnit : Units, IMonster
    {
        private readonly int hitRate = 60;
        private int hitTimer = 0;
        private float expGiven;

        public MeleUnit(Vector2 pos, float speed, bool act, float att, float def, float hp, float exp, bool alive, float range) : base(pos, speed, range)
        {
            this.Attack = att;
            this.Defence = def;
            this.Health = hp;
            this.ExpGiven = exp;
            this.Alive = alive;
            this.Active = act;
        }

        public float ExpGiven
        {
            get
            {
                return this.expGiven;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Enemies' experience given cannot be a negative number!",(int)value);
                }
                this.expGiven = value;
            }
        }

        public bool Active { get; set; }

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