namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Screens;
    using Interfaces;
    using Microsoft.Xna.Framework.Audio;

    public class Heroes : Units , IShootable, ILevelable
    {
        private int ammo = 0;
        private int firingTimer = 0;
        private float fireRate = 20;

        private KeyboardState keyboard;
        private MouseState mouse;

        private SoundEffect walk;
        private SoundEffectInstance walkInstance;
        private SoundEffect walk2;
        private SoundEffectInstance walkInstance2;
        private SoundEffect gunShot;
        private SoundEffectInstance gunShotInstance;
       
        public Heroes(Vector2 pos, float speed) : base(pos,speed)
        {
            Content.RootDirectory = "Content";
            walk = Content.Load<SoundEffect>(@"Textures\Sounds\pl_dirt1");
            walk2 = Content.Load<SoundEffect>(@"Textures\Sounds\pl_dirt2");
            gunShot = Content.Load<SoundEffect>(@"Textures\Sounds\gunShot");
            walkInstance = walk.CreateInstance();
            walkInstance.IsLooped = false;
            walkInstance.Volume = 0.1f;
            walkInstance2 = walk2.CreateInstance();
            walkInstance2.IsLooped = false;
            walkInstance2.Volume = 0.1f;
            gunShotInstance = gunShot.CreateInstance();
            gunShotInstance.IsLooped = false;
            gunShotInstance.Volume = 0.1f;
        }


        public Heroes(Vector2 pos, float speed, float HP, float Att, float Def) : this(pos,speed)
        {
            this.Health = HP;
            this.Attack = Att;
            this.Defence = Def;
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

        public override void Update()
        {
            this.keyboard = Keyboard.GetState();
            this.mouse = Mouse.GetState();
            Vector2 oldPos = this.Position;

            if (this.keyboard.IsKeyDown(Keys.W))
            {
                if (oldPos.Y > GameScreen.PRoom.Y+20)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y - this.Speed);
                    GameScreen.CharacterPosition = this.Position;

                    walkInstance.Play();
                    walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.A))
            {
                if (oldPos.X > GameScreen.PRoom.X+20)
                {
                    this.Position = new Vector2(oldPos.X - this.Speed, oldPos.Y);
                    GameScreen.CharacterPosition = this.Position;
                    walkInstance.Play();
                    walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.S) )
            {
                if (oldPos.Y < GameScreen.PRoom.Height-90)
                {
                    this.Position = new Vector2(oldPos.X, oldPos.Y + this.Speed);
                    GameScreen.CharacterPosition = this.Position;
                    walkInstance.Play();
                    walkInstance2.Play();
                }
            }

            if (this.keyboard.IsKeyDown(Keys.D))
            {
                if (oldPos.X < GameScreen.PRoom.Width-50)
                {
                    this.Position = new Vector2(oldPos.X + this.Speed, oldPos.Y);
                    GameScreen.CharacterPosition = this.Position;
                    walkInstance.Play();
                    walkInstance2.Play();
                }
            }

            oldPos = this.Position;

            this.Rotation = this.PointDirecions(this.Position.X, this.Position.Y, this.mouse.X, this.mouse.Y);

            this.PreviousKeyboard = this.keyboard;            
        }

        public override void CheckShooting()
        {
            if (this.FiringTimer > this.FireRate)
            {
                this.FiringTimer = 0;
                this.Shoot();
                gunShot.Play();
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
    }
}