namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public class Cursor : Obj
    {
        MouseState mouse;

        public Cursor(Vector2 pos)
            : base(pos)
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
