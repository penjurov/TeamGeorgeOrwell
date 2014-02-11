namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Obj
    {
        private readonly float scale = 0.7f;

        public Obj(Vector2 pos)
        {
            this.Position = pos;
        }

        public Vector2 Position { get; set; }

        public Texture2D SpriteIndex { get; set; }
           
        public bool Alive { get; set; }

        public virtual void LoadContent(ContentManager content, string sprName)
        {
            this.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", sprName));
        }
       
        public virtual void Draw(SpriteBatch spriteBatch, Viewport viewport, float rotation)
        {
            Vector2 center = new Vector2(this.SpriteIndex.Width / 2, this.SpriteIndex.Height / 2);

            spriteBatch.Draw(this.SpriteIndex, this.Position, null, Color.White, 
                MathHelper.ToRadians(rotation), center, this.scale, SpriteEffects.None, 0);
        }
     
    }
}