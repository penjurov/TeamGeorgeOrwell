namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class Characters : Obj, ISkillable
    {      
        public Characters(Vector2 pos) : base(pos)
        {
            
        }

        public Skills Skill { get; set; }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }
    
    }
}