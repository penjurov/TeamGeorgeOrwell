using System;
using System.Linq;
using Rpg.Objects;

namespace Rpg.Interfaces
{
    public interface ISkillable
    {
        Skills Skill { get; set; }
    }
}