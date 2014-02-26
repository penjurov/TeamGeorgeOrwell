namespace Rpg.Screens
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;
    
    internal class ChooseHeroScreen : Screen
    {
        private static string heroName;
        private readonly Cursor cursor = new Cursor(new Position(0, 0)); 
        private readonly IList<Texture2D> heroTexture = new List<Texture2D>();        
        private IList<MenuItems> chooseHeroList = new List<MenuItems>();
        
        private int selectedEntry = 0;

        private Texture2D chooseHeroBackgroundTexture;
        private Vector2 chooseHeroBackgroundPosition;

        private Vector2 heroPicturesPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;
        private bool inMenu = false;

        public static string HeroName
        {
            get
            {
                return heroName;
            }

            private set
            {
                heroName = value;
            }
        }

        public IList<MenuItems> PchooseHeroList
        {
            get
            {
                return this.chooseHeroList;
            }

            set
            {
                this.chooseHeroList = value;
            }
        }

        public override void LoadObjects(ContentManager content)
        {
            this.chooseHeroBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\MainMenu");

            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_odin"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_thor"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_eir"));

            this.LoadCursor(content);
        }

        public override void DrawObjects(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Text");
            this.chooseHeroBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.chooseHeroBackgroundTexture, this.chooseHeroBackgroundPosition, Color.White);

            if (this.PchooseHeroList.Count < 3)
            {
                // Odin
                this.heroPicturesPosition = new Vector2(10, 130);
                this.PchooseHeroList.Add(new MenuItems(this.heroTexture[0], this.heroPicturesPosition, "ODIN", newFont, false));
                      
                // Thor
                this.heroPicturesPosition.X += this.heroTexture[0].Width + 10;
                this.PchooseHeroList.Add(new MenuItems(this.heroTexture[1], this.heroPicturesPosition, "THOR", newFont, false));

                // Eir
                this.heroPicturesPosition.X += this.heroTexture[1].Width + 10;
                this.PchooseHeroList.Add(new MenuItems(this.heroTexture[2], this.heroPicturesPosition, "EIR", newFont, false));
            }

            this.PchooseHeroList[this.selectedEntry].Selected = true;
            foreach (var item in this.PchooseHeroList)
            {
                item.DrawMenuItems(spriteBatch, Color.DeepSkyBlue);
            }

            this.DrawCursor(spriteBatch);
            spriteBatch.End();
        }

        public override void UpdateObjects(ContentManager content)
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            this.UpdateCursor();

            if (this.keyboard.IsKeyDown(Keys.Right) && this.previousKeyboard.IsKeyUp(Keys.Right))
            {
                this.chooseHeroList[this.selectedEntry].Selected = false;
                if (this.selectedEntry < this.chooseHeroList.Count - 1)
                {
                    this.selectedEntry++;
                }

                this.chooseHeroList[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Left) && this.previousKeyboard.IsKeyUp(Keys.Left))
            {
                this.chooseHeroList[this.selectedEntry].Selected = false;
                if (this.selectedEntry > 0)
                {
                    this.selectedEntry--;
                }

                this.chooseHeroList[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter))
            {
                if (this.inMenu)
                {
                    HeroName = this.chooseHeroList[this.selectedEntry].ItemText;
                    Rpg.PActiveWindow=EnumActiveWindow.GameWindow;
                }
            }

            if (this.previousMouse.LeftButton == ButtonState.Released && this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in this.chooseHeroList)
                {
                    if (this.mouse.X > item.ItemPosition.X && this.mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                        this.mouse.Y > item.ItemPosition.Y && this.mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)
                    {
                        HeroName = item.ItemText;
                        Rpg.PActiveWindow=EnumActiveWindow.GameWindow;
                    }
                }
            }

            this.previousMouse = this.mouse;
            this.previousKeyboard = this.keyboard;
            this.inMenu = true;
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