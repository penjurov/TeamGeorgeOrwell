namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Interfaces;

    public class Bullet : Obj, IMovable
    {
        private float speed;

        public Bullet(Vector2 pos, Texture2D texture) : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
        }

        public bool Alive { get; set; }

        public float Rotation { get; set; }
 
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
      
        public void Update()
        {
            if (this.Alive)
            {
                this.PushTo(this.Speed, this.Rotation);
            }          
        }

        private void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            this.Position += new Vector2(pix * newX, pix * newY);
        }

    }
}