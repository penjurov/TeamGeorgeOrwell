namespace Rpg.Interfaces
{
    using Objects;

    public interface IPlayer
    {
        int Level { get; set; }

        float CurrentExp { get; set; }

        float MaxMP { get; set; }

        float MaxHP { get; set; }

        float Mana { get; set; }

        Skill Skill { get; set; }
    }
}