namespace Rpg.Objects
{ 
    using Interfaces;

    public class MeleUnit : Units, IMonster
    {
        private float expGiven;

        public MeleUnit(Position pos, float speed, bool act, float att, float def, float hp, float exp, bool alive, float range) : base(pos, speed, range)
        {
            this.Attack = att;
            this.Defence = def;
            this.Health = hp;
            this.ExpGiven = exp;
            this.Alive = alive;
            this.Active = act;
            this.HitRate = 60;
        }

        public float ExpGiven
        {
            get
            {
                return this.expGiven;
            }

            private set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Enemies' experience given cannot be a negative number!", (int)value);
                }

                this.expGiven = value;
            }
        }

        public bool Active { get; set; }
    }
}