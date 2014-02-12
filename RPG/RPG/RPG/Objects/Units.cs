namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Interfaces;
    using Screens;

    public abstract class Units : Obj, ISkillable, IMovable, IShootable
    {      
        public Units(Vector2 pos,float speed) : base(pos)
        {
            this.Speed = speed;
        }

        public float Speed { get; set; }

        public float Rotation {get; set;}      

        public Skills Skill { get; set; }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }

        public bool Alive { get; set; }

        public abstract int FiringTimer { get; set; }

        public abstract float FireRate { get; set; }

        public abstract void Update();

        public abstract void CheckShooting();
      
    }
}