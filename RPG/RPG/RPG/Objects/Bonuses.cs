﻿namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;

    public class Bonuses : Obj
    {
        public Bonuses(Vector2 pos) : base(pos)
        {
            this.Position = pos;
        }

        public int Quantity { get; set; }

        public int SpawnTime { get; set; }
    }
}