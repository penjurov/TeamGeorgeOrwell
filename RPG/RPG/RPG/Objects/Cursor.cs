namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Interfaces;

    public class Cursor : Obj, IMovable
    {
        private MouseState mouse;

        public Cursor(Vector2 pos) : base(pos)
        {
            this.Position = pos;
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();

            this.Position = new Vector2(this.mouse.X, this.mouse.Y);
        }
    }
}