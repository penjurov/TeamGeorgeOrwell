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

    public class Bullet : Obj
    {
        public Bullet(Vector2 pos, Texture2D texture)
            : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
