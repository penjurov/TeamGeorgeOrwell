namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;

    public class Cursor : Obj
    {
        public Cursor(Vector2 pos)
            : base(pos)
        {
            this.Position = pos;
        }
    }
}