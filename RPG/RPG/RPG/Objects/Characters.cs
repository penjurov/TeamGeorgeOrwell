namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Characters : Obj
    {
        private float movingSpeed = 5;
        private float fireRate = 20;

        public Characters(Vector2 pos, float movSpeed) : base(pos)
        {
            this.MovingSpeed = movSpeed;    
        }

        public float MovingSpeed
        {
            get
            {
                return this.movingSpeed;
            }

            set
            {
                this.movingSpeed = value;
            }
        }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }

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
    }
}