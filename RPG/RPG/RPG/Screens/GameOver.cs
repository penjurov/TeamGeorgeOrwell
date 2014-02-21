namespace Rpg.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;   

    class GameOver
    {
        private Texture2D gameOverScreenBackgroundTexture;
        private Vector2 gameOverScreenBackgroundPosition;

        private KeyboardState keyboard;
        private MouseState mouse;

        public void Load(ContentManager content)
        {
            this.gameOverScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\GameOver");
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            this.gameOverScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.gameOverScreenBackgroundTexture, this.gameOverScreenBackgroundPosition, Color.White);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Title");
            Vector2 namePosition = new Vector2(400, 300);
            spriteBatch.DrawString(font, "Game Over", namePosition, Color.White);
          
            spriteBatch.End();
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            if (this.keyboard.IsKeyDown(Keys.Space))
            {
                Environment.Exit(1);
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                Environment.Exit(1);
            }
        }
    }
}