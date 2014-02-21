namespace Rpg.Interfaces
{
    using System;
    using System.Linq;

    public interface IMonster
    {
        bool Active { get; set; }

        float ExpGiven { get; }
    }
}