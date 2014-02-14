using System;
using System.Linq;

namespace Rpg.Interfaces
{
    public interface IUpdatable
    {
        float Speed { get; }

        float Rotation { get; set; }

        bool Alive { get; set; }

        void Update();
    }
}