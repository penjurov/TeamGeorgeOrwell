namespace Rpg
{ 
    public abstract class GameStarter
    {
        private static void Main()
        {
            Rpg game = new Rpg();           
            game.Run();            
        }
    }
}