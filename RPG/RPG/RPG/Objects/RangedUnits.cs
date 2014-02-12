namespace Rpg.Objects
{
<<<<<<< HEAD
    using System;
=======
    using System;
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;

<<<<<<< HEAD
    public class RangedUnits : MeleUnits, IShootable
=======
    public class RangedUnits : MeleUnits, IShootable
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
    {
        private int firingTimer = 0;
        private float fireRate = 20;

<<<<<<< HEAD
        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
=======
        public RangedUnits(Vector2 pos, float speed) : base(pos, speed)
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
        {
            //To add stats to the unit: health, attack, defence, skills, experience to give.
        }

        public float FireRate
        {
<<<<<<< HEAD
            get
            {
                return this.fireRate;
            }
=======
            get
            {
                return this.fireRate;
            }
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
            set
            {
                this.fireRate = value;
            }
        }

        public int FiringTimer
        {
<<<<<<< HEAD
            get
            {
                return this.firingTimer;
            }
=======
            get
            {
                return this.firingTimer;
            }
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
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