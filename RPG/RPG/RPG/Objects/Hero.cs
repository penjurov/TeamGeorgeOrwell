namespace Rpg.Objects
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public class Hero : Units, IShootable, IPlayer
    {
        private int firingTimer = 0;
        private float fireRate = 20;

        public Hero(Vector2 pos, float speed, float hp, float att, float def, float range) : base(pos, speed, range)
        {
            this.Health = hp;
            this.Attack = att;
            this.Defence = def;
        }
        
        public float CurrentExp { get; set; }

        public int Level { get; set; }

        protected float FireRate
        {
            get
            {
                return this.fireRate;
            }

            private set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The fire rate of unit cannot be a negative number!", (int)value);
                }
                this.fireRate = value;
            }
        }

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
            if (this.FiringTimer > this.FireRate)
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
                    bullet.Position = this.Position;
                    bullet.Area = this.Area;
                    bullet.Alive = true;                    
                    bullet.Rotation = this.Rotation;
                    bullet.Speed = 10;
                    break;
                }
            }
        }
    }
}