namespace Rpg.Screens
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    class AboutScreen
    {
        private readonly IList<MenuItems> aboutScreenItems = new List<MenuItems>();
        private readonly IList<Texture2D> planketTexture = new List<Texture2D>();
        private readonly int selectedEntry = 0;

        private Texture2D aboutScreenBackgroundTexture;
        private Vector2 aboutScreenBackgroundPosition;

        private Vector2 planketPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;

        public void Load(ContentManager content)
        {
            this.aboutScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\MainMenu");

            this.planketTexture.Add(content.Load<Texture2D>(@"Textures\GameScreens\MainMenuPlank"));
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            this.aboutScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.aboutScreenBackgroundTexture, this.aboutScreenBackgroundPosition, Color.White);

            if (this.aboutScreenItems.Count < 1)
            {
                // Back planket and text;
                this.planketPosition = new Vector2(700, 600);
                this.aboutScreenItems.Add(new MenuItems(this.planketTexture[0], this.planketPosition, "Back", newFont, false));
            }

            this.aboutScreenItems[this.selectedEntry].Selected = true;
            foreach (var item in this.aboutScreenItems)
            {
                item.Draw(spriteBatch);
            }

            SpriteFont font = content.Load<SpriteFont>(@"Fonts/Names");
            Vector2 namePosition = new Vector2(10, 10);
            spriteBatch.DrawString(font, "Iliya Pendzhurov", namePosition, Color.White);

            namePosition = new Vector2(10, 110);
            spriteBatch.DrawString(font, "Stoyan Stoyanov", namePosition, Color.White);

            namePosition = new Vector2(10, 210);
            spriteBatch.DrawString(font, "Dobromir Brezoev", namePosition, Color.White);

            namePosition = new Vector2(10, 310);
            spriteBatch.DrawString(font, "Dimitar Paskov", namePosition, Color.White);

            namePosition = new Vector2(10, 410);
            spriteBatch.DrawString(font, "Cvetan Gerginski", namePosition, Color.White);

            namePosition = new Vector2(10, 510);
            spriteBatch.DrawString(font, "Angel Velikov", namePosition, Color.White);

            spriteBatch.End();
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();            

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter) 
                && this.aboutScreenItems.Count != 0)
            {
                if (this.aboutScreenItems[this.selectedEntry].ItemText == "Back")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
                }
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in this.aboutScreenItems)
                {
                    if (this.mouse.X > item.ItemPosition.X && this.mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                        this.mouse.Y > item.ItemPosition.Y && this.mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)
                    {
                        if (item.ItemText == "Back")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
                            break;
                        }
                    }
                }
            }

            this.previousKeyboard = this.keyboard;
        }
    }
}
