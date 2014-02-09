namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Obj
    {
        private readonly float scale = 1.0f;
        private float rotation = 0.0f;
        private float speed = 0.0f;

        public Obj()
        {
        }

        public Obj(Vector2 pos)
        {
            this.Position = pos;
        }

        public Vector2 Position { get; set; }

        public Texture2D SpriteIndex { get; set; }

        public string SpriteName { get; set; }

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

        public bool Alive { get; set; }

        public virtual void LoadContent(ContentManager content, string sprName)
        {
            this.SpriteName = sprName;
            this.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", sprName));
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
            this.Position += new Vector2(pix * newX, pix * newY);
        }
    }
}