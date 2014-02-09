namespace Rpg.Screens
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    internal class MainMenuScreen
    {
        private static List<MenuItems> mainMenuItems = new List<MenuItems>();

        private readonly List<Texture2D> planketTexture = new List<Texture2D>();
        private int selectedEntry = 0;

        private Texture2D mainMenuBackgroundTexture;
        private Vector2 mainMenuBackgroundPosition;

        private Vector2 planketPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;

        public static List<MenuItems> PMainMenuItems
        {
            get
            {
                return mainMenuItems;
            }

            set
            {
                mainMenuItems = value;
            }
        }

        public void Load(ContentManager content)
        {
            this.mainMenuBackgroundTexture = content.Load<Texture2D>(@"Textures\MainMenu\MainMenu");

            this.planketTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank"));
            this.planketTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank02"));
            this.planketTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank03"));
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            // empty variable ????
            Color color = Color.Black;

            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            this.mainMenuBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.mainMenuBackgroundTexture, this.mainMenuBackgroundPosition, Color.White);

            if (PMainMenuItems.Count < 4)
            {
                // New game planket and text;
                this.planketPosition = new Vector2(700, 260);
                PMainMenuItems.Add(new MenuItems(this.planketTexture[2], this.planketPosition, "New game", newFont, false));

                // Control planket and text
                this.planketPosition.Y += 80;
                PMainMenuItems.Add(new MenuItems(this.planketTexture[1], this.planketPosition, "Controls", newFont, false));

                // About planket and text
                this.planketPosition.Y += 80;
                PMainMenuItems.Add(new MenuItems(this.planketTexture[2], this.planketPosition, "About", newFont, false));

                // Exit game planket and text
                this.planketPosition.Y += 80;
                PMainMenuItems.Add(new MenuItems(this.planketTexture[0], this.planketPosition, "Exit game", newFont, false));
            }

            PMainMenuItems[this.selectedEntry].Selected = true;
            foreach (var item in PMainMenuItems)
            {
                if (true)
                {
                    item.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            if (this.keyboard.IsKeyDown(Keys.Down) && this.previousKeyboard.IsKeyUp(Keys.Down))
            {
                PMainMenuItems[this.selectedEntry].Selected = false;
                if (this.selectedEntry < PMainMenuItems.Count - 1)
                {
                    this.selectedEntry++;
                }

                PMainMenuItems[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Up) && this.previousKeyboard.IsKeyUp(Keys.Up))
            {
                PMainMenuItems[this.selectedEntry].Selected = false;
                if (this.selectedEntry > 0)
                {
                    this.selectedEntry--;
                }

                PMainMenuItems[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter))
            {
                if (PMainMenuItems[this.selectedEntry].ItemText == "New game" || PMainMenuItems[this.selectedEntry].ItemText == "Resume game")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                }

                if (PMainMenuItems[this.selectedEntry].ItemText == "Exit game")
                {
                    Environment.Exit(1);
                }
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in PMainMenuItems)
                {
                    if (this.mouse.X > item.ItemPosition.X && this.mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                        this.mouse.Y > item.ItemPosition.Y && this.mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)
                    {
                        if (item.ItemText == "New game" || item.ItemText == "Resume game")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                        }

                        if (item.ItemText == "Exit game")
                        {
                            Environment.Exit(1);
                        }
                    }
                }
            }

            this.previousKeyboard = this.keyboard;
        }
    }
}