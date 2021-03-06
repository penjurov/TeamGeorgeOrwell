namespace Rpg.Objects
{
    using System;

    public class Skill : Obj
    {
        private readonly int cost = 100;

        public Skill(Position pos, SkillType type, int power) : base(pos)
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

        public int Power { get; private set; }

        public SkillType Type { get; private set; }
    }
}