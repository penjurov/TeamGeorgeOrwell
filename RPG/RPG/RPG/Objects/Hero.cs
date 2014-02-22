namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Interfaces;
    using Microsoft.Xna.Framework;
    
    public class Hero : Units, IShootable, IPlayer
    {
        // Singleton
        private static Hero instance;

        private int firingTimer = 0;
        private float fireRate = 20;
        private float mana;
        private float maxHP;
        private float maxMP;
        private Skills heroSkill;

        private Hero(Position pos, float speed, float hp, float att, float def, float range, float mp, SkillType skill) : base(pos, speed, range)
        {
            this.Health = hp;
            this.MaxHP = hp;
            this.Attack = att;
            this.Defence = def;
            this.mana = mp;
            this.maxMP = mp;
            this.Level = 1;
            this.HeroSkill = new Skills(pos, skill);
        }
     
        public float CurrentExp { get; set; }

        public int Level { get; set; }

        public float MaxHP
        {
            get
            {
                return this.maxHP;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Hero's maximum health cannot be a negative number!", (int)value);
                }

                this.maxHP = value;
            }
        }

        public int FiringTimer
        {
            get
            {
                return this.firingTimer;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The firing timer of unit cannot be a negative number!", value);
                }

                this.firingTimer = value;
            }
        }

        public float Mana
        {
            get
            {
                return this.mana;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Hero's mana cannot be a negative number!", (int)value);
                }

                this.mana = value;
            }
        }

        public float MaxMP
        {
            get
            {
                return this.maxMP;
            }

            set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("Hero's maximum mana cannot be a negative number!", (int)value);
                }

                this.maxMP = value;
            }
        }

        protected float FireRate
        {
            get
            {
                return this.fireRate;
            }

            private set
            {
                if (value < 0)
                {
                    throw new NegativeDataException("The fire rate of unit cannot be a negative number!", (int)value);
                }

                this.fireRate = value;
            }
        }

        public Skills HeroSkill
        {
            get
            {
                return this.heroSkill;
            }
            set
            {
                this.heroSkill = value;
            }
        }

        // Singleton
        public static Hero Instance(Position pos, float speed, float hp, float att, float def, float range, float mp, SkillType skill)
        {
            if (instance == null)
            {
                instance = new Hero(pos, speed, hp, att, def, range, mp, skill);
            }

            return instance;
        }

        public void CheckShooting(IList<Bullet> bullets)
        {
            if (this.FiringTimer > this.FireRate)
            {
                this.FiringTimer = 0;
                this.Shoot(bullets);                
            }
        }

        private void Shoot(IList<Bullet> bullets)
        {
            foreach (var bullet in bullets)
            {
                if (!bullet.Alive)
                {
                    bullet.Position = this.Position;
                    bullet.Area = this.Area;
                    bullet.Alive = true;                    
                    bullet.Rotation = this.Rotation;
                    bullet.Speed = 10;
                    break;
                }
            }
        }
    }
}