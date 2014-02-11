using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rpg.Interfaces
{
    public interface IShootable
    {
        int FiringTimer { get; set; }

        float FireRate { get; set; }

        void CheckShooting();

    }
}
