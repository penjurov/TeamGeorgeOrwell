namespace Rpg.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;
    
    internal class WinScreen : Screen
    {
        private readonly Cursor cursor = new Cursor(new Position(0, 0)); 

        private Texture2D winScreenBackgroundTexture;
        private Vector2 winScreenBackgroundPosition;
        private KeyboardState keyboard;
        private MouseState mouse;

        public override void LoadObjects(ContentManager content)
        {
            this.LoadCursor(content);
            this.winScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\Win");
        }

        public override void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.Black);
            this.winScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.winScreenBackgroundTexture, this.winScreenBackgroundPosition, Color.White);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Title");
            Vector2 namePosition = new Vector2(400, 300);
            spriteBatch.DrawString(font, "You won!", namePosition, Color.White);

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