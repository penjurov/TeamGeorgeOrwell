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

    public class Obj
    {
        private Vector2 position;
        private Texture2D spriteIndex;

        private float rotation = 0.0f;
        private string spriteName;
        private float speed = 0.0f;
        private float scale = 1.0f;
        private bool alive;

        public Obj()
        {
        }

        public Obj(Vector2 pos)
        {
            this.position = pos;
        }     

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public Texture2D SpriteIndex
        {
            get
            {
                return this.spriteIndex;
            }

            set
            {
                this.spriteIndex = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this.rotation;
            }

            set
            {
                this.rotation = value;
            }
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

        public bool Alive
        {
            get
            {
                return this.alive;
            }

            set
            {
                this.alive = value;
            }
        }

        public virtual void LoadContent(ContentManager content, string sprName)
        {
            this.spriteName = sprName;
            this.spriteIndex = content.Load<Texture2D>(@"Textures\Objects\" + sprName);
        }

        public virtual void Update()
        {
            if (!this.Alive)
            {
                return; 
            }

            this.PushTo(this.Speed, this.Rotation);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Viewport viewport)
        {
            Vector2 center = new Vector2(this.SpriteIndex.Width / 2, this.SpriteIndex.Height / 2);

            spriteBatch.Draw(this.SpriteIndex, this.Position, null, Color.White, MathHelper.ToRadians(this.Rotation), center, this.scale, SpriteEffects.None, 0);
        }

        private void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            this.Position += new Vector2(pix * (float)newX, pix * (float)newY);
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
