namespace Rpg.Objects
{
    using System.Collections;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Screens;

    public class Heroes : Units, IShootable, ILevelable, IMovable
    {
        private int ammo = 0;
        private int firingTimer = 0;
        private float fireRate = 20;
       
        public Heroes(Vector2 pos, float speed, float hp, float att, float def) : base(pos, speed)
        {
            this.Health = hp;
            this.Attack = att;
            this.Defence = def;
        }

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

        public float CurrentExp { get; set; }

        public int Level { get; set; }

        public int Ammo 
        {
            get
            {
                return this.ammo;
            }

            set
            {
                this.ammo = value;
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