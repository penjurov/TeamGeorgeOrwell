namespace Rpg.Interfaces
{
    using Objects;

    public interface IUnit
    {
        Skills Skill { get; set; }

        float Range { get; }
        
        float Health { get; set; }

        float Attack { get; }

        float Defence { get; }
    }
}