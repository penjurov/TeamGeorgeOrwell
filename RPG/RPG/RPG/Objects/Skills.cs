namespace Rpg.Objects
{
    using System;

    public class Skills : Obj
    {
        private SkillType type;
        private int level = 1;
        private int cost = 100;
        private int power;


        public Skills(Position pos, SkillType type, int power, int cost) : base(pos)
        {
            this.Position = pos;
            this.Level = 1;
            this.Cost = cost;
            this.Type = type;
            this.Power = power;
        }

        public int Level 
        { 
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        public int Cost
        {
            get
            {
                return this.cost;
            }
            set
            {
                this.cost = value;
            }
        }

        public int Power
        {
            get
            {
                return this.power;
            }
            set
            {
                this.power = value;
            }
        }

        public SkillType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}