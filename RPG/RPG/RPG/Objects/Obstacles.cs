namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;  

    public class Obstacles : Obj
    {
        public Obstacles(Vector2 pos, Texture2D texture)
            : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
        }

        public int Length { get; set; }

        public int Rotation { get; set; }

        public float Angle { get; set; }
    }
}