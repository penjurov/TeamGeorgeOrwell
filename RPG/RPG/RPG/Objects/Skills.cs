namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;

    public class Skills : Obj
    {
        private int skillLevel;
        private int skillCost;

        public Skills(Vector2 pos) : base(pos)
        {
            this.Position = pos;
            this.SkillLevel = 1;
            this.skillCost = 50;
        }

        public void UseSkill()
        {

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