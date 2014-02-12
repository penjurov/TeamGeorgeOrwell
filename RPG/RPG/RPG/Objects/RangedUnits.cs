namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Microsoft.Xna.Framework.Content;
    using Screens;
    using System;

    public class RangedUnits : MeleUnits , IShootable
    {
        private int firingTimer = 0;
        private float fireRate = 20;

        public RangedUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            //To add stats to the unit: health, attack, defence, skills, experience to give.

        }

        public float FireRate
        {
            get { return this.fireRate; }
            set
            {
                this.fireRate = value;
            }
        }

        public int FiringTimer
        {
            get { return this.firingTimer; }
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

        private float PointDirecions(float x, float y, float x2, float y2)
        {
            float divX = x - x2;
            float divY = y - y2;
            float adj = divX;
            float opp = divY;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
            {
                res += 360;
            }

            return res;
        }
    }
}
