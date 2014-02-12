namespace Rpg.Objects
{
    using Microsoft.Xna.Framework;
    using Interfaces;

    public class RangedUnits : MeleUnits , IShootable
    {
        public RangedUnits(Vector2 pos, float speed)
            : base(pos, speed)
        {
            //To add stats to the unit.

        }

        public float FireRate { get; set; }

        public int FiringTimer { get; set; }

        public void CheckShooting()
        {
            //TO DO:
        }

        private void Shoot()
        {
            //TO DO:
        }        
    }
}
