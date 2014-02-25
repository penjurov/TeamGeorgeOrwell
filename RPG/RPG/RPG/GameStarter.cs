namespace Rpg
{ 
    public abstract class GameStarter
    {
        static void Main()
        {
            Rpg game = new Rpg();           
            game.Run();            
        }
    }
}