namespace RPG
{
    #region Using Statements
    using System;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    #endregion

    public class GameScreen : Screens
    {
        private Texture2D backgroundTexture;
        private Texture2D characterTexture;
        private Vector2 backgroundPosition;
        private Vector2 characterPosition;
        private int verticalPosition = 0;
        private int horizontalPosition = 0;

        public GameScreen()
            : base()
        {
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\GameScreen");
            characterTexture = content.Load<Texture2D>(@"Textures\Characters\male");


            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            //backgroundPosition = new Vector2((viewport.Width - backgroundTexture.Width) / 2, (viewport.Height - backgroundTexture.Height) / 2);
            backgroundPosition = new Vector2((viewport.Width - backgroundTexture.Width) / 2, 0);
            characterPosition = new Vector2((viewport.Width + 20) / 2, 0);
            base.LoadContent();
        }
        public override void HandleInput()
        {
            int oldSelectedEntry = selectedEntry;

            if (InputManager.IsActionTriggered(Action.MainMenu))
            {
                ScreenManager.AddScreen(new MainMenuScreen());
            }

            if (InputManager.IsActionTriggered(Action.MoveCharacterDown))
            {
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                
                verticalPosition += 10;
                characterPosition = new Vector2(((viewport.Width + 20) / 2) + horizontalPosition, verticalPosition);
            }

            if (InputManager.IsActionTriggered(Action.MoveCharacterUp))
            {
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                verticalPosition -= 10;
                characterPosition = new Vector2(((viewport.Width + 20) / 2) + horizontalPosition, verticalPosition);
            }

            if (InputManager.IsActionTriggered(Action.MoveCharacterLeft))
            {
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                horizontalPosition -= 10;
                characterPosition = new Vector2(((viewport.Width + 20) / 2) + horizontalPosition, verticalPosition);
            }

            if (InputManager.IsActionTriggered(Action.MoveCharacterRight))
            {
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;

                horizontalPosition += 10;
                characterPosition = new Vector2(((viewport.Width + 20) / 2) + horizontalPosition, verticalPosition);
            }

            base.HandleInput();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            // draw the background images
            
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            spriteBatch.Draw(characterTexture, characterPosition, Color.White);
            Vector2 textPosition = new Vector2(10, 10);
            spriteBatch.DrawString(Fonts.DescriptionFont,
                    "Score" , textPosition, Color.White);
           
            spriteBatch.End();
        }
    }
}
