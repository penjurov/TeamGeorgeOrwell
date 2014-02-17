namespace Rpg.Screens
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    public class ChooseHeroScreen
    {
        private static string heroName;
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

        public void Load(ContentManager content)
        {
            this.chooseHeroBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\MainMenu");

            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_odin"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_thor"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\ChooseHero\select_eir"));
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
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
                item.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

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
                    Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
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
                        Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                    }
                }
            }

            this.previousMouse = this.mouse;
            this.previousKeyboard = this.keyboard;
            this.inMenu = true;
        }
    }
}
