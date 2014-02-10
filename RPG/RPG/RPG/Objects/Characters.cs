namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class Characters : Obj
    {      
        public Characters(Vector2 pos) : base(pos)
        {
            
        }
      
        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }
    
    }
}