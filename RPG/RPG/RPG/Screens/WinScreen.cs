namespace Rpg.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    
    class WinScreen
    {
        private Texture2D winScreenBackgroundTexture;
        private Vector2 winScreenBackgroundPosition;
        private KeyboardState keyboard;
        private MouseState mouse;

        public void Load(ContentManager content)
        {
            this.winScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\Win");
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.Black);
            this.winScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.winScreenBackgroundTexture, this.winScreenBackgroundPosition, Color.White);

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Title");
            Vector2 namePosition = new Vector2(400, 300);
            spriteBatch.DrawString(font, "You won!", namePosition, Color.White);

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