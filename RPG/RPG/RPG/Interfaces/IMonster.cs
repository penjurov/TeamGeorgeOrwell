using System;
using System.Linq;

namespace Rpg.Interfaces
{
    public interface IMonster
    {
        bool Active { get; set; }

        float ExpGiven { get; }
    }
}