namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;

    public enum SkillType { Heal, CollateralDamage, Hit };

    public class Skills : Obj
    {
        private int skillLevel;
        private int skillCost;
        private readonly SkillType skillType;

        public Skills(Vector2 pos, SkillType skillType)
            : base(pos)
        {
            this.Position = pos;
            this.SkillLevel = 1;
            this.SkillCost = 50;
            this.skillType = skillType;
        }

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

        public int SkillLevel 
        {
            get
            {
                return this.skillLevel;
            }
            set
            {
                this.skillLevel = value;
            }
        }

        public int SkillCost 
        {
            get
            {
                return this.skillCost;
            }
            set
            {
                this.skillCost = value;
            }
        }
    }
}