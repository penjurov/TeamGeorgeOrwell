namespace Rpg.Objects
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public abstract class ShootingUnit : Units, IShooting
    {
        private int firingTimer;
        private float fireRate;

        protected ShootingUnit(Position pos, float speed, float range):base(pos,speed,range)
        {

        }
        protected float FireRate
        {
            get
            {
                return this.fireRate;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The firing rate of unit cannot be a negative number!", (int)value);
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
            if (this.FiringTimer > fireRate)
            {
                this.FiringTimer = 0;
                this.Shoot(bullets);
            }
        }

        protected abstract void Shoot(IList<Bullet> bullets);
        //{
        //    foreach (var bullet in bullets)
        //    {
        //        if (!bullet.Alive)
        //        {
        //            bullet.Alive = true;
        //            bullet.Position = this.Position;
        //            bullet.Rotation = this.Rotation;
        //            bullet.Speed = 5;
        //            break;
        //        }
        //    }
        //}
    }
}