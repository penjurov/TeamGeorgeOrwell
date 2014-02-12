namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Interfaces;
    using Screens;

    public abstract class Units : Obj, ISkillable, IMovable
    {      
        public Units(Vector2 pos,float speed) : base(pos)
        {
            this.Speed = speed;
        }

        public float Speed { get; set; }

        public float Rotation {get; set;}      

        public Skills Skill { get; set; }

        public float Health { get; set; }

        public float Attack { get; set; }

        public float Defence { get; set; }

        public bool Alive { get; set; }

        public virtual void Update()
        {
            float characterX = GameScreen.PRoom.Width / 2;
            float characterY = GameScreen.PRoom.Height / 2;

            this.Rotation = this.PointDirecions(Camera.GlobalToLocal(this.Position).X,
               Camera.GlobalToLocal(this.Position).Y, characterX, characterY);

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

    }
}