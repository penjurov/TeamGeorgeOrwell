namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Cursor : Obj
    {
        private MouseState mouse;

        public Cursor(Vector2 pos) : base(pos)
        {
            this.Position = pos;
        }

        public override void Update()
        {
            this.mouse = Mouse.GetState();

            this.Position = new Vector2(this.mouse.X, this.mouse.Y);

            base.Update();
        }
    }
}