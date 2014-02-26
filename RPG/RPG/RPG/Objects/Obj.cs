namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Obj
    {
        protected Obj(Position pos)
        {
            this.Position = pos;
        }

        public Position Position { get; set; }

        public Rectangle Area { get; set; }

        public Texture2D SpriteIndex { get; set; }

        public bool Alive { get; set; }
    }
}