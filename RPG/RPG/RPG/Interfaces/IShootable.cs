using System;
using System.Linq;

namespace Rpg.Interfaces
{
    public interface IShootable
    {
        int FiringTimer { get; set; }

        float FireRate { get; set; }

        void CheckShooting();
    }
}