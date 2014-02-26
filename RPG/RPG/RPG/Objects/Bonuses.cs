namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics; 
    
    public class Bonuses : Obj
    {
        private const int Spawn = 500;

        public Bonuses(Position pos, Texture2D texture, string type, Rectangle area) : base(pos)
        {
            this.Position = pos;
            this.SpriteIndex = texture;
            this.Type = type;
            this.Area = area;
            this.SpawnTime = Spawn;
            this.Alive = true;
        }

        public string Type { get; set; }

        public int SpawnTime { get; set; }
    }
}