namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class MeleUnits : Units
    {
        public MeleUnits(Vector2 pos, float speed) : base(pos,speed)
        {
            //To add stats: health, attack, defence, skills, experience to give.
        }

<<<<<<< HEAD
        public float ExpGiven { get; set; }
=======
        public float ExpGiven { get; set; }
>>>>>>> a000f1b78253d7360fff18b0d83e0e273e560eed
    }
}