namespace Rpg.Screens
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class ChooseHeroScreen
    {
        private static List<MenuItems> chooseHeroList = new List<MenuItems>();
        private readonly List<Texture2D> heroTexture = new List<Texture2D>();

        private int selectedEntry = 0;

        private Texture2D chooseHeroBackgroundTexture;
        private Vector2 chooseHeroBackgroundPosition;

        private Vector2 heroPicturesPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;

        public void Load(ContentManager content)
        {
            this.chooseHeroBackgroundTexture = content.Load<Texture2D>(@"Textures\MainMenu\MainMenu");

            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank02"));
            this.heroTexture.Add(content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank03"));

        }

        public void Draw()
        {

        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            if (this.keyboard.IsKeyDown(Keys.Right) && this.previousKeyboard.IsKeyUp(Keys.Right))
            {
                chooseHeroList[this.selectedEntry].Selected = false;
                if (this.selectedEntry < chooseHeroList.Count - 1)
                {
                    this.selectedEntry++;
                }

                chooseHeroList[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Left) && this.previousKeyboard.IsKeyUp(Keys.Left))
            {
                chooseHeroList[this.selectedEntry].Selected = false;
                if (this.selectedEntry > 0)
                {
                    this.selectedEntry--;
                }

                chooseHeroList[this.selectedEntry].Selected = true;
            }

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter))
            {
                Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in chooseHeroList)
                {
                    if (this.mouse.X > item.ItemPosition.X && this.mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                        this.mouse.Y > item.ItemPosition.Y && this.mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)
                    {
                        if (item.ItemText == "New game")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.ChooseHeroWindow);
                            break;
                        }

                        if (item.ItemText == "Resume game")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                            break;
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
