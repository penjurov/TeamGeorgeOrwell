using System;
using System.Linq;

namespace Rpg.Interfaces
{
    public interface IMovable
    {
        float Speed { get; }

        float Rotation { get; set; }

        bool Alive { get; set; }
    }
}