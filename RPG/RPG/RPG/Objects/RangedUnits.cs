namespace Rpg.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;

    public class RangedUnits : MeleUnits, IShootable
    {
        private int firingTimer = 0;
        private float fireRate = 20;

        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
        {
            //To add stats to the unit: health, attack, defence, skills, experience to give.
        }

        public float FireRate
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

        public int FiringTimer
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

        public void CheckShooting()
        {
            if (this.FiringTimer > this.FireRate)
            {
                this.FiringTimer = 0;
                this.Shoot();
            }
        }

        private void Shoot()
        {
            foreach (var bullet in GameScreen.EnemyBullets)
            {
                if (!bullet.Alive)
                {
                    bullet.Alive = true;
                    bullet.Position = this.Position;
                    bullet.Rotation = this.Rotation;
                    bullet.Speed = 20;
                    break;
                }
            }
        }
    }
}