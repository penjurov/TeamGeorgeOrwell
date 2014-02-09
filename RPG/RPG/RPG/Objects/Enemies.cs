namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    public class Enemies : Characters
    {
        private Skills skill;
        private float expGiven;

        public Enemies(Vector2 pos, float movSpeed)
            : base(pos, movSpeed)
        {
        }

        public Skills Skill
        {
            get
            {
                return this.skill;
            }

            set
            {
                this.skill = value;
            }
        }

        public float ExpGiven
        {
            get
            {
                return this.expGiven;
            }

            set
            {
                this.expGiven = value;
            }
        }
    }
}
