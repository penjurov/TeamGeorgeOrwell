namespace RPG
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Heroes : Characters
    {
        private int ammo = 0;
        private int firingTimer = 0;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;
      
        public Heroes(Vector2 pos, float movSpeed) : base(pos, movSpeed)
        {
            this.Position = pos;
            this.MovingSpeed = movSpeed;
        }

        public Skills Skill { get; set; }

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

        public override void Update()
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

            this.previousMouse = this.mouse;
            this.previousKeyboard = this.keyboard;
            base.Update();
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
            foreach (var bullet in GameScreen.Bullets)
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
    }
}