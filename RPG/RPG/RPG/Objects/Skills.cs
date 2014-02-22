namespace Rpg.Objects
{
    using System;

    public class Skills : Obj
    {
        private readonly SkillType skillType;

        public Skills(Position pos, SkillType skillType) : base(pos)
        {
            this.Position = pos;
            this.SkillLevel = 1;
            this.SkillCost = 50;
            this.skillType = skillType;
        }

        public int SkillLevel { get; set; }

        public int SkillCost { get; set; }

        public void UseSkill()
        {
            switch (this.skillType)
            {
                case SkillType.Heal:
                    break;
                case SkillType.CollateralDamage:
                    break;
                case SkillType.Hit:
                    break;
                default:
                    break;
            }
        }

        public void LevelUp()
        {
            this.SkillLevel++;
        }
    }
}