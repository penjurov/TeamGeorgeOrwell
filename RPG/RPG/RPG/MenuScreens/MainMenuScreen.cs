namespace RPG
{
    #region Using Statements
    using System;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    #endregion

    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    public class MainMenuScreen : Screens
    {
        #region Graphics Data

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;
        private Vector2 descriptionAreaTextPosition;
        private Vector2 selectPosition;
        private Texture2D plankTexture1, plankTexture2, plankTexture3;

        #endregion

        #region Menu Entries

        MenuEntry newGameMenuEntry, exitGameMenuEntry, controlsMenuEntry, helpMenuEntry;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base()
        {
            // add the New Game entry
            newGameMenuEntry = new MenuEntry("New Game");
            newGameMenuEntry.Description = "Start a New Game";
            newGameMenuEntry.Font = Fonts.HeaderFont;
            newGameMenuEntry.Position = new Vector2(715, 0f);            
            MenuEntries.Add(newGameMenuEntry);

            // add the Controls menu entry
            controlsMenuEntry = new MenuEntry("Controls");
            controlsMenuEntry.Description = "View Game Controls";
            controlsMenuEntry.Font = Fonts.HeaderFont;
            controlsMenuEntry.Position = new Vector2(720, 0f);         
            MenuEntries.Add(controlsMenuEntry);

            // add the Help menu entry
            helpMenuEntry = new MenuEntry("Help");
            helpMenuEntry.Description = "View Game Help";
            helpMenuEntry.Font = Fonts.HeaderFont;
            helpMenuEntry.Position = new Vector2(700, 0f);
            MenuEntries.Add(helpMenuEntry);

            // create the Exit menu entry
            exitGameMenuEntry = new MenuEntry("Exit");
            exitGameMenuEntry.Description = "Quit the Game";
            exitGameMenuEntry.Font = Fonts.HeaderFont;
            exitGameMenuEntry.Position = new Vector2(720, 0f);
            MenuEntries.Add(exitGameMenuEntry);

            // Music
        }


        /// <summary>
        /// Load the graphics content for this screen.
        /// </summary>
        public override void LoadContent()
        {
            // load the textures
            ContentManager content = ScreenManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>(@"Textures\MainMenu\MainMenu");
            //descriptionAreaTexture =
            //    content.Load<Texture2D>(@"Textures\MainMenu\MainMenuInfoSpace");
            //iconTexture = content.Load<Texture2D>(@"Textures\MainMenu\GameLogo");
            plankTexture1 =
                content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank");
            plankTexture2 =
                content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank02");
            plankTexture3 =
                content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank03");

            // calculate the texture positions
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                (viewport.Width - backgroundTexture.Width) / 2,
                (viewport.Height - backgroundTexture.Height) / 2);
            descriptionAreaTextPosition = backgroundPosition + new Vector2(158, 350);
            selectPosition = backgroundPosition + new Vector2(1120, 610);

            // set the textures on each menu entry
            newGameMenuEntry.Texture = plankTexture3;
            controlsMenuEntry.Texture = plankTexture2;
            helpMenuEntry.Texture = plankTexture3;
            exitGameMenuEntry.Texture = plankTexture1;

            // now that they have textures, set the proper positions on the menu entries
            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntries[i].Position = new Vector2(
                    MenuEntries[i].Position.X,
                    500f - ((MenuEntries[i].Texture.Height - 10) *
                        (MenuEntries.Count - 1 - i)));
            }

            base.LoadContent();
        }

        #endregion

        #region Updating


        /// <summary>
        /// Handles user input.
        /// </summary>
        public override void HandleInput()
        {
            int oldSelectedEntry = selectedEntry;

            // Move to the previous menu entry?
            if (InputManager.IsActionTriggered(Action.CursorUp))
            {
                selectedEntry--;
                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (InputManager.IsActionTriggered(Action.CursorDown))
            {
                selectedEntry++;
                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            // Press Enter
            if (InputManager.IsActionTriggered(Action.Ok))
            {
                if (menuEntries[selectedEntry].Text == "Exit")
                {
                    Environment.Exit(1);
                }
                else if (menuEntries[selectedEntry].Text == "New Game")
                {
                    ScreenManager.AddScreen(new GameScreen());
                }
                else
                {
                    MessageBox.Show(menuEntries[selectedEntry].Text);
                }

            }
        }

        #endregion

        #region Drawing


        /// <summary>
        /// Draw this screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            // draw the background images
            spriteBatch.Draw(backgroundTexture, backgroundPosition, Color.White);
            
            // Draw each menu entry in turn.
            for (int i = 0; i < MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = MenuEntries[i];
                bool isSelected = (i == selectedEntry);
                menuEntry.Draw(this, isSelected, gameTime);
            }

            // draw the description text for the selected entry
            MenuEntry selectedMenuEntry = SelectedMenuEntry;
            if ((selectedMenuEntry != null) &&
                !String.IsNullOrEmpty(selectedMenuEntry.Description))
            {
                Vector2 textSize =
                    Fonts.DescriptionFont.MeasureString(selectedMenuEntry.Description);
                Vector2 textPosition = new Vector2(10, 10);
                spriteBatch.DrawString(Fonts.DescriptionFont,
                    selectedMenuEntry.Description, textPosition, Color.White);
            }

            spriteBatch.End();
        }

        #endregion
    }
}
