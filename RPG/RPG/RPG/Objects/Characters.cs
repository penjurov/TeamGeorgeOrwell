namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public class Characters : Obj
    {
        private float health;
        private float movingSpeed = 5;
        private float attack;
        private float defence;
        private float fireRate = 20;

        public Characters(Vector2 pos, float movSpeed)
            : base(pos)
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

        public float Health
        {
            get
            {
                return this.health;
            }

            set
            {
                this.health = value;
            }
        }

        public float Attack
        {
            get
            {
                return this.attack;
            }

            set
            {
                this.attack = value;
            }
        }

        public float Defence
        {
            get
            {
                return this.defence;
            }

            set
            {
                this.defence = value;
            }
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
    }
}
