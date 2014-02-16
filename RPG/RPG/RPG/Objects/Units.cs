namespace Rpg.Objects
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework;
    
    public abstract class Units : Obj
    {
        private int hitRate = 60;
        private int hitTimer = 0;

        public Units(Vector2 pos, float speed, bool act, float range) : base(pos)
        {
            this.Speed = speed;
            this.Alive = true;
            this.Active = act;
            this.Range = range;
        }

        public bool Active { get; set; }

        public float Range { get; set; }

        public float ExpGiven { get; set; }

        public float Speed { get; set; }

        public float Rotation { get; set; }

        public Skills Skill { get; set; }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }

        public virtual int FiringTimer { get; set; }

        public virtual float FireRate { get; set; }

        public int HitRate
        {
            get
            {
                return this.hitRate;
            }
            set
            {
                this.hitRate = value;
            }
        }

        public int HitTimer
        {
            get
            {
                return this.hitTimer;
            }
            set
            {
                this.hitTimer = value;
            }
        }

        public virtual void CheckShooting(IList<Bullet> bullets)
        {

        }

    }
}