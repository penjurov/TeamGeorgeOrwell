namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;

    public class Cursor : Obj
    {
        public Cursor(Position pos) : base(pos)
        {
            this.Position = pos;
        }
    }
}