using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rpg.Interfaces
{
    public interface IMonster
    {
        bool Active { get; set; }

        float ExpGiven { get; }
    }
}