namespace Rpg.Objects
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework;
    
    public abstract class Units : Obj
    {      
        public Units(Vector2 pos, float speed) : base(pos)
        {
            this.Speed = speed;
        }

        public float ExpGiven { get; set; }

        public float Speed { get; set; }

        public float Rotation { get; set; }

        public Skills Skill { get; set; }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }

        public bool Alive { get; set; }

        public abstract int FiringTimer { get; set; }

        public abstract float FireRate { get; set; }

        public virtual void Update() 
        {
            
        }

        public abstract void CheckShooting(IList<Bullet> bullets);

    }
}