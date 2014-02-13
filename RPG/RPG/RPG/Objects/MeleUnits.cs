namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Screens;

    public class MeleUnits : Units
    {
        public MeleUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            //To add stats: health, attack, defence, skills, experience to give.
        }

        public float ExpGiven { get; set; }

        public override float FireRate { get; set; }

        public override int FiringTimer { get; set; }

        public override void Update()
        {
            this.Rotation = this.PointDirecions(this.Position.X, this.Position.Y,
                GameScreen.CharacterPosition.X, GameScreen.CharacterPosition.Y);

            this.PushTo(this.Speed, this.Rotation);
        }

        private float PointDirecions(float x, float y, float x2, float y2)
        {
            float divX = x - x2;
            float divY = y - y2;
            float adj = divX;
            float opp = divY;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
            {
                res += 360;
            }

            return res;
        }        

        private void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            this.Position += new Vector2(pix * newX, pix * newY);
        }

        public override void CheckShooting()
        {

        }
        
    }
}