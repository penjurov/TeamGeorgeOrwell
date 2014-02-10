namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Screens;
    using Interfaces;

    public class Heroes : Characters , IShootable, IMovable, ISkillable
    {
        private int ammo = 0;
        private int firingTimer = 0;
        private float movingSpeed = 5;
        private float fireRate = 20;
        private float speed = 0.0f;

        private KeyboardState keyboard;
        private MouseState mouse;
       
        public Heroes(Vector2 pos, float movSpeed) : base(pos)
        {
            this.Position = pos;
            this.MovingSpeed = movSpeed;
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

        public float MovingSpeed
        {
            get
            {
                return this.movingSpeed;
            }

            private set
            {
                this.movingSpeed = value;
            }
        }

        public float FireRate
        {
            get
            {
                return this.fireRate;
            }

            private set
            {
                this.fireRate = value;
            }
        }

        public Skills Skill { get; set; }

        public float CurrentExp { get; set; }

        public int Level { get; set; }

        public MouseState PreviousMouse { get; set; }

        public KeyboardState PreviousKeyboard { get; set; }

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

        public void Update()
        {
            this.keyboard = Keyboard.GetState();
            this.mouse = Mouse.GetState();
            Vector2 oldPos = this.Position;

            if (this.keyboard.IsKeyDown(Keys.W))
            {
                if (oldPos.Y > 0)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y - this.MovingSpeed);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.A))
            {
                if (oldPos.X > -500)
                {
                    this.Position = new Vector2(oldPos.X - this.MovingSpeed, oldPos.Y);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.S))
            {
                if (oldPos.Y < 2000)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y + this.MovingSpeed);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.D))
            {
                if (oldPos.X < 1500)
                {
                    this.Position = new Vector2(oldPos.X + this.MovingSpeed, oldPos.Y);
                }
            }

            oldPos = this.Position;

            this.Rotation = this.PointDirecions(Camera.GlobalToLocal(this.Position).X, Camera.GlobalToLocal(this.Position).Y, this.mouse.X, this.mouse.Y);

            this.PreviousMouse = this.mouse;
            this.PreviousKeyboard = this.keyboard;

            if (!this.Alive)
            {
                return;
            }

            this.PushTo(this.Speed, this.Rotation);
        }

        public void CheckShooting()
        {
            if (this.FiringTimer > this.FireRate && this.Ammo > 0)
            {
                this.FiringTimer = 0;
                this.Shoot();
            }
        }

        public void Shoot()
        {
            foreach (var bullet in GameScreen.PBullets)
            {
                if (!bullet.Alive)
                {
                    this.Ammo--;
                    bullet.Alive = true;
                    bullet.Position = this.Position;
                    bullet.Rotation = this.Rotation;
                    bullet.Speed = 10;
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

        private void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            this.Position += new Vector2(pix * newX, pix * newY);
        }
    }
}