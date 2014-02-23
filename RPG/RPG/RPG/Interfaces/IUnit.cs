namespace Rpg.Interfaces
{
    using Objects;

    public interface IUnit
    {
        float Range { get; }
        
        float Health { get; set; }

        float Attack { get; }

        float Defence { get; }
    }
}