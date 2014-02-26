namespace Rpg.Objects
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public class Bullet : Obj, IMovable
    {
        public Bullet(Position pos, Texture2D texture) : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
        }

        public float Rotation { get; set; }
 
        public float Speed { get; set; }
    }
}