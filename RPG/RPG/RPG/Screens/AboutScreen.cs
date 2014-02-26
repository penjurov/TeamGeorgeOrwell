namespace Rpg.Screens
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;

    internal class AboutScreen : Screen
    {
        private readonly IList<MenuItems> aboutScreenItems = new List<MenuItems>();
        private readonly Cursor cursor = new Cursor(new Position(0, 0)); 
        private readonly int selectedEntry = 0;

        private Texture2D button;
        private Texture2D aboutScreenBackgroundTexture;
        private Vector2 aboutScreenBackgroundPosition;

        private Vector2 buttonPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;

        public override void LoadObjects(ContentManager content)
        {
            this.aboutScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\About");

            this.button = content.Load<Texture2D>(@"Textures\GameScreens\Button");

            this.LoadCursor(content);
        }

        public override void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Text");
            this.aboutScreenBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.aboutScreenBackgroundTexture, this.aboutScreenBackgroundPosition, Color.White);

            if (this.aboutScreenItems.Count < 1)
            {
                // Back planket and text;
                this.buttonPosition = new Vector2(840, 660);
                this.aboutScreenItems.Add(new MenuItems(this.button, this.buttonPosition, "Back", newFont, false));
            }

            this.aboutScreenItems[this.selectedEntry].Selected = true;
            foreach (var item in this.aboutScreenItems)
            {
                item.DrawMenuItems(spriteBatch, new Color(248, 218, 127));
            }

            this.DrawCursor(spriteBatch);
            spriteBatch.End();
        }

        public override void UpdateObjects(ContentManager content)
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            this.UpdateCursor();

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter) &&
                this.aboutScreenItems.Count != 0)
            {
                if (this.aboutScreenItems[this.selectedEntry].ItemText == "Back")
                {
                    Rpg.PActiveWindow=EnumActiveWindow.MainMenu;
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
                            Rpg.PActiveWindow=EnumActiveWindow.MainMenu;
                            break;
                        }
                    }
                }
            }

            this.previousKeyboard = this.keyboard;
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
