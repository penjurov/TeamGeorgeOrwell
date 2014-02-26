namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Interfaces;

    public class RangedUnit : Units, IMonster, IShooting
    {
        private readonly float fireRate = 60;
        private int firingTimer = 0;
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

        public int FiringTimer
        {
            get
            {
                return this.firingTimer;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The firing timer of unit cannot be a negative number!", value);
                }

                this.firingTimer = value;
            }
        }

        public void CheckShooting(IList<Bullet> bullets)
        {
            if (this.FiringTimer > this.fireRate)
            {
                this.FiringTimer = 0;
                this.Shoot(bullets);
            }
        }

        private void Shoot(IList<Bullet> bullets)
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