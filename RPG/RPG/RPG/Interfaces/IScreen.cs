namespace Rpg.Interfaces
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    
    public interface IScreen
    {
        void LoadObjects(ContentManager content);

        void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content);

        void UpdateObjects(ContentManager content);
    }
}