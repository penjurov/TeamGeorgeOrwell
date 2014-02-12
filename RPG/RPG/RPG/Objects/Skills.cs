namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class Skills : Obj
    {
        public Skills(Vector2 pos) : base(pos)
        {
            this.Position = pos;
        }
    }
}