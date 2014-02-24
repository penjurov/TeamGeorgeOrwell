namespace Rpg.Objects
{
    using System.Collections.Generic;
    using Interfaces;
    
    public class Hero : ShootingUnits, IPlayer
    {
        // Singleton
        private static Hero instance;

        private const int Timer = 0;
        private const float Rate = 40;
        private float mana;
        private float maxHP;
        private float maxMP;

        private Hero(Position pos, float speed, float hp, float att, float def, float range, float mp, SkillType skillType, int skillPower) : base(pos, speed, range)
        {
            this.Health = hp;
            this.MaxHP = hp;
            this.Attack = att;
            this.Defence = def;
            this.mana = mp;
            this.maxMP = mp;
            this.Level = 1;
            this.FireRate = Rate;
            this.FiringTimer = Timer;
            this.Skill = new Skills(pos, skillType, skillPower);
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

        public Skills Skill { get; set; }


        // Singleton
        public static Hero Instance(Position pos, float speed, float hp, float att, float def, float range, float mp, SkillType skillType, int skillPower)
        {
            if (instance == null)
            {
                instance = new Hero(pos, speed, hp, att, def, range, mp, skillType, skillPower);
            }

            return instance;
        }
    }
}