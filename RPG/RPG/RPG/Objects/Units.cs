namespace Rpg.Objects
{
    using Interfaces;
    using Microsoft.Xna.Framework;

    public abstract class Units : Obj, IUnit, IMovable
    {
        private float range;
        private float speed;
        private float rotation;
        private float health;
        private float attack;
        private float defence;

        public Units(Position pos, float speed, float range) : base(pos)
        {
            this.Speed = speed;
            this.Alive = true;
            this.Range = range;
        }

        public float Range
        {
            get
            {
                return this.range;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Enemy range cannot be a negative number!", (int)value);
                }

                this.range = value;
            }
        }

        public float Speed
        {
            get
            {
                return this.speed;
            }

            set
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
                    throw new NegativeDataException("The rotation of unit cannot be a negative number!", (int)value);
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

            set
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

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The defence of unit cannot be a negative number!", (int)value);
                }

                this.defence = value;
            }
        }

        public virtual int HitRate { get; protected set; }

        public virtual int HitTimer { get; set; }
    }
}