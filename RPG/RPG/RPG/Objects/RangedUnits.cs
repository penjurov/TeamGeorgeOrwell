namespace Rpg.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;

    public class RangedUnits : Units
    {
        private int firingTimer = 0;
        private float fireRate = 80;

        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
        {
            //To add stats to the unit: health, attack, defence, skills, experience to give.
        }

        public  float FireRate
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

        public  int FiringTimer
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
        public override void Update()
        {
            this.FiringTimer++;
            if (Math.Abs(GameScreen.CharacterPosition.X - this.Position.X) < 200 &&
                Math.Abs(GameScreen.CharacterPosition.Y - this.Position.Y) < 200)
            {
                base.Update();
                this.Shoot();
            }
        }

        private  void Shoot()
        {
            if (this.FiringTimer > this.FireRate)
            {
                this.FiringTimer = 0;
                foreach (var bullet in GameScreen.EnemyBullets)
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
}