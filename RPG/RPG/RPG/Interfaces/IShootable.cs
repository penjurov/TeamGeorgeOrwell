namespace Rpg.Interfaces
{   
    using System.Collections.Generic;
    using Objects;

    public interface IShootable
    {
        int FiringTimer { get; set; }

        float FireRate { get; set; }

        void CheckShooting(IList<Bullet> bullets);
    }
}