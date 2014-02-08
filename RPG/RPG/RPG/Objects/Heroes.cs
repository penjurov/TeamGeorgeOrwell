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

namespace RPG
{
    public class Heroes : Characters
    {
        private Skills skill;
        private float currentExp;
        private int level;
        private int ammo = 0;
        private int firingTimer = 0;


        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;

        
        public Heroes(Vector2 pos, float movSpeed)
            : base(pos, movSpeed)
        {
            Position = pos;
            MovingSpeed = movSpeed;
        }

        public Skills Skill
        {
            get
            {
                return this.skill;
            }
            set
            {
                this.skill = value;
            }
        }

        public float CurrentExp
        {
            get
            {
                return this.currentExp;
            }
            set
            {
                this.currentExp = value;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

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
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            Vector2 oldPos = Position;

            if (keyboard.IsKeyDown(Keys.W))
            {
                if (oldPos.Y > 0)
                {
                    Position = new Vector2(oldPos.X, oldPos.Y - MovingSpeed);
                }
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                if (oldPos.X > -500)
                {
                    Position = new Vector2(oldPos.X - MovingSpeed, oldPos.Y);
                }
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                if (oldPos.Y < 1000)
                {
                    Position = new Vector2(oldPos.X, oldPos.Y + MovingSpeed);
                }
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                if (oldPos.X < 1500)
                {
                    Position = new Vector2(oldPos.X + MovingSpeed, oldPos.Y);
                }
            }

            oldPos = Position;

            Rotation = PointDirecions(Camera.globalToLocal(Position).X, Camera.globalToLocal(Position).Y, mouse.X, mouse.Y);


            previousMouse = mouse;
            previousKeyboard = keyboard;
            base.Update();
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

        public void CheckShooting()
        {
            if (FiringTimer > FireRate && Ammo > 0)
            {
                FiringTimer = 0;
                Shoot();
            }
        }

        public void Shoot()
        {
            foreach (var bullet in RPG.bullets)
            {
                if (!bullet.Alive)
                {
                    Ammo--;
                    bullet.Alive = true;
                    bullet.Position = Position;
                    bullet.Rotation = Rotation;
                    bullet.Speed = 10;
                    break;   
                }              
            }           
        }
    }
}
