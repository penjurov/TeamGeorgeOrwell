namespace Rpg.Screens
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Interfaces;

    public abstract class Screen : IScreen
    {
        public abstract void LoadObjects(Microsoft.Xna.Framework.Content.ContentManager content);

        public abstract void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content);

        public abstract void UpdateObjects(ContentManager content);
    }
}