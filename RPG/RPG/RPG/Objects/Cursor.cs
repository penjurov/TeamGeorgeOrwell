namespace Rpg.Objects
{
    using System;

    public class Cursor : Obj
    {
        public Cursor(Position pos) : base(pos)
        {
            this.Position = pos;
        }
    }
}