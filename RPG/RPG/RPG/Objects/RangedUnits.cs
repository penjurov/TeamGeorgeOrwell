namespace Rpg.Objects
{
    using System;
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;

    public class RangedUnits : Units,IBoostable
    {
        private int firingTimer = 0;
        private float fireRate = 80;
        private float exp;

        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
        {
            this.Attack = 0;
            this.Defence = 0;
            this.Health = 0;
            this.ExpGiven = 0;
        }

        public float ExpGiven
        {
            get
            {
                return this.exp;
            }
            private set
            {
                this.exp = value;
            }
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