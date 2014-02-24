namespace Rpg.Objects
{
    using Interfaces;
    using System.Collections.Generic;

    public class RangedUnit : ShootingUnit, IMonster
    {
        public const int Timer=0;
        public const float Rate=60;
        private float expGiven;

        public RangedUnit(Position pos, float speed, bool act, float att, float def, float hp, float exp, bool alive, float range) : base(pos, speed, range)
        {
            this.Attack = att;
            RangeAtk = this.Attack;
            this.Defence = def;
            this.Health = hp;
            this.ExpGiven = exp;
            this.Alive = alive;
            this.Active = act;
            this.FireRate= Rate;
            this.FiringTimer = Timer;
        }

        public static float RangeAtk { get; private set; }

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
                    throw new NegativeDataException("Enemies' experience given cannot be a negative number!", (int)value);
                }

                this.expGiven = value;
            }
        }

        public bool Active { get; set; }

        protected  override void Shoot(IList<Bullet> bullets)
        {
            foreach (var bullet in bullets)
            {
                if (!bullet.Alive)
                {
                    bullet.Alive = true;
                    bullet.Position = this.Position;
                    bullet.Rotation = this.Rotation;
                    bullet.Speed = 5;
                    break;
                }
            }
        }
    }
}