namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Screens;
    using Interfaces;

    public class Heroes : Units , IShootable
    {
        private int ammo = 0;
        private int firingTimer = 0;
        private float fireRate = 20;

        private KeyboardState keyboard;
        private MouseState mouse;
       
        public Heroes(Vector2 pos, float speed) : base(pos,speed)
        {

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

        public float CurrentExp { get; set; }

        public int Level { get; set; }

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

        public override void Update()
        {
            this.keyboard = Keyboard.GetState();
            this.mouse = Mouse.GetState();
            Vector2 oldPos = this.Position;

            if (this.keyboard.IsKeyDown(Keys.W))
            {
                if (oldPos.Y > GameScreen.PRoom.Y)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y - this.Speed);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.A))
            {
                if (oldPos.X > GameScreen.PRoom.X)
                {
                    this.Position = new Vector2(oldPos.X - this.Speed, oldPos.Y);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.S) )
            {
                if (oldPos.Y < GameScreen.PRoom.Height)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y + this.Speed);
                }
            }

            if (this.keyboard.IsKeyDown(Keys.D))
            {
                if (oldPos.X < GameScreen.PRoom.Width)
                {
                    this.Position = new Vector2(oldPos.X + this.Speed, oldPos.Y);
                }
            }

            oldPos = this.Position;

            this.Rotation = this.PointDirecions(Camera.GlobalToLocal(this.Position).X, Camera.GlobalToLocal(this.Position).Y, this.mouse.X, this.mouse.Y);

            this.PreviousKeyboard = this.keyboard;            
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
            foreach (var bullet in GameScreen.PBullets)
            {
                if (!bullet.Alive)
                {
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

        // UNUSED ?????
        private void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            this.Position += new Vector2(pix * newX, pix * newY);
        }
    }
}