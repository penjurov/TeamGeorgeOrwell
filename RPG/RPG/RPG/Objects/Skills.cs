namespace Rpg.Objects
{
    using System;

    public class Skills : Obj
    {
        private SkillType type;
        private readonly int cost = 100;
        private int power;


        public Skills(Position pos, SkillType type, int power) : base(pos)
        {
            this.Position = pos;
            this.Type = type;
            this.Power = power;
        }

        public int Cost
        {
            get
            {
                return this.cost;
            }
        }

        public int Power
        {
            get
            {
                return this.power;
            }
            private set
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
            private set
            {
                this.type = value;
            }
        }
    }
}