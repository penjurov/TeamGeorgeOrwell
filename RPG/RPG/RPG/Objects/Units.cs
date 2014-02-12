namespace Rpg.Objects
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Interfaces;
    using Screens;

    public abstract class Units : Obj, ISkillable, IMovable,IShootable
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

<<<<<<< HEAD
        public abstract int FiringTimer { get; set; }
=======
        public override void Update(Viewport viewport)
        {
            this.Rotation = this.PointDirecions(this.Position.X,this.Position.Y, 
                GameScreen.CharacterPosition.X,GameScreen.CharacterPosition.Y);
>>>>>>> 71dabed0cb897f544d2914a7bf9eb0416f51d0cc

        public abstract float FireRate { get; set; }

        public abstract void Update();

        public abstract void CheckShooting();
      
    }
}