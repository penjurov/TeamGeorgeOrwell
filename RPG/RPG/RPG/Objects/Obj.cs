namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Obj
    {
        public Obj()
        {
        }

        public Obj(Vector2 pos)
        {
            this.Position = pos;
        }

        public Vector2 Position { get; set; }

        public Texture2D SpriteIndex { get; set; }               
    }
}