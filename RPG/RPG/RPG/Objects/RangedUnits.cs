namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Screens;
    
    public class RangedUnits : Units, IShootable
    {
        private int firingTimer = 0;
        private float fireRate = 80;

        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
        {
            this.Health = 210;
            this.Defence = 30;
            Damage = 150;
            this.ExpGiven = 180;
            this.Alive = true;
        }

        public static float Damage { get; private set; }

        public override float FireRate
        {
            get
            {
                return this.fireRate;
            }

            set
            {
                this.fireRate = value;
            }
        }

        public override int FiringTimer
        {
            get
            {
                return this.firingTimer;
            }

            set
            {
                this.firingTimer = value;
            }
        }

        public override void CheckShooting(IList<Bullet> bullets)
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