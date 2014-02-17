namespace Rpg.Objects
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;   
    
    public class Bullet : Obj, IMovable
    {
        private float speed;

        public Bullet(Vector2 pos, Texture2D texture) : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
        }

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
    }
}