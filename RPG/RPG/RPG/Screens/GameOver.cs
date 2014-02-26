namespace Rpg.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;

    internal class GameOver : Screen
    {
        private readonly Cursor cursor = new Cursor(new Position(0, 0)); 
        private Texture2D gameOverScreenBackgroundTexture;
        private Vector2 gameOverScreenBackgroundPosition;
        private KeyboardState keyboard;
        private MouseState mouse;

        public override void LoadObjects(ContentManager content)
        {
            this.LoadCursor(content);
            this.gameOverScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\GameOver");
        }

        public override void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            this.gameOverScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.gameOverScreenBackgroundTexture, this.gameOverScreenBackgroundPosition, Color.White);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Title");
            Vector2 namePosition = new Vector2(400, 300);
            spriteBatch.DrawString(font, "Game Over", namePosition, Color.White);

            this.DrawCursor(spriteBatch);
            spriteBatch.End();
        }

        public override void UpdateObjects(ContentManager content)
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            this.UpdateCursor();

            if (this.keyboard.IsKeyDown(Keys.Space))
            {
                Environment.Exit(1);
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                Environment.Exit(1);
            }
        }

        private void LoadCursor(ContentManager content)
        {
            this.cursor.SpriteIndex = content.Load<Texture2D>(string.Format(@"Textures\Objects\{0}", "cursor"));
        }

        private void DrawCursor(SpriteBatch spriteBatch)
        {
            Vector2 cursPos = new Vector2(this.cursor.Position.X, this.cursor.Position.Y);
            spriteBatch.Draw(this.cursor.SpriteIndex, cursPos, Color.White);
        }

        private void UpdateCursor()
        {
            this.cursor.Position = new Position(this.mouse.X, this.mouse.Y);
        }
    }
}