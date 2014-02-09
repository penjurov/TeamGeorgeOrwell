namespace RPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Obsticles : Obj
    {
        private int length;
        private float angle;

        public int Length
        {
            get
            {
                return this.length;
            }

            set
            {
                this.length = value;
            }
        }

        public float Angle
        {
            get
            {
                return this.angle;
            }

            set
            {
                this.angle = value;
            }
        }
    }
}
