namespace Rpg.Interfaces
{ 
    using System.Collections.Generic;
    using Objects;

    public interface IShooting
    {
        int FiringTimer { get; set; }

        void CheckShooting(IList<Bullet> bullets);
    }
}