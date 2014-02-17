namespace Rpg.Objects
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    
    public abstract class Units : Obj, ISkillable
    {
        private float range;
        private float expGiven;
        private float speed;
        private float rotation;
        private float health;
        private float attack;
        private float defence;

        public Units(Vector2 pos, float speed, bool act, float range) : base(pos)
        {
            this.Speed = speed;
            this.Alive = true;
            this.Active = act;
            this.Range = range;
        }

        public bool Active { get; set; }

        public float Range
        {
            get
            {
                return this.range;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Enemy range cannot be a negative number!",(int)value);
                }

                this.range = value;
            }
        }

        public float ExpGiven
        {
            get
            {
                return this.expGiven;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Enemies' experience given cannot be a negative number!",(int)value);
                }
                this.expGiven = value;
            }
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Speed of the unit cannot be a negative number!", (int)value);
                }
                this.speed = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this.rotation;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The rotation of unit cannot be a negative number!",(int)value);
                }
                this.rotation = value;
            }
        }

        public Skills Skill { get; set; }

        public float Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Unit's health cannot be a negative number!", (int)value);
                }
                this.health = value;
            }
        }

        public float Attack 
        {
            get
            {
                return this.attack;
            }

            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The attack of unit cannot be a negative number!", (int)value);
                }
                this.attack = value;
            }
        }

        public float Defence
        {
            get
            {
                return this.defence;
            }

            protected set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The defence of unit cannot be a negative number!", (int)value);
                }
                this.defence = value;
            }
        }

        public virtual int FiringTimer { get; set; }

        public virtual float FireRate { get; protected set; }

        public virtual void CheckShooting(IList<Bullet> bullets)
        {
        }

        public virtual int HitRate { get; protected set; }

        public virtual int HitTimer { get; set; }
    }
}