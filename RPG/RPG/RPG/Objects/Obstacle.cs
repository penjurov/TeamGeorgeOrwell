namespace Rpg.Objects
{
    using Microsoft.Xna.Framework.Graphics;  

    public class Obstacle : Obj
    {
        public Obstacle(Position pos, Texture2D texture, bool vis) : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
            this.Visible = vis;
        }

        public bool Visible { get; set; }

        public int Rotation { get; set; }
    }
}