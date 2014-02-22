﻿namespace Rpg.Screens
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Objects;

    internal class MainMenuScreen
    {
        private static IList<MenuItems> mainMenuItems = new List<MenuItems>();
        private readonly Cursor cursor = new Cursor(new Position(0, 0)); 

        private IList<Texture2D> buttons = new List<Texture2D>();
     
        private int selectedEntry = 0;
        private Texture2D mainMenuBackgroundTexture;
        private Vector2 mainMenuBackgroundPosition;
        private Vector2 buttonPosition;
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;

        public static IList<MenuItems> PMainMenuItems
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
            this.mainMenuBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\MainMenu");

            this.buttons.Add(content.Load<Texture2D>(@"Textures\GameScreens\Button"));
            this.buttons.Add(content.Load<Texture2D>(@"Textures\GameScreens\Button_first"));
            this.buttons.Add(content.Load<Texture2D>(@"Textures\GameScreens\Button_last"));

            this.LoadCursor(content);
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        { 
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            this.mainMenuBackgroundPosition = new Vector2(0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(this.mainMenuBackgroundTexture, this.mainMenuBackgroundPosition, Color.White);

            if (PMainMenuItems.Count < 4)
            {
                // New game planket and text;
                this.buttonPosition = new Vector2(392, 224);
                PMainMenuItems.Add(new MenuItems(this.buttons[1], this.buttonPosition, "NEW GAME", newFont, false));

                // Control planket and text
                this.buttonPosition.X = 453;

                this.buttonPosition.Y += 50;
                PMainMenuItems.Add(new MenuItems(this.buttons[0], this.buttonPosition, "CONTROLS", newFont, false));

                // About planket and text
                this.buttonPosition.X = 453;
                this.buttonPosition.Y += 50;
                PMainMenuItems.Add(new MenuItems(this.buttons[0], this.buttonPosition, "ABOUT", newFont, false));

                // Exit game planket and text
                this.buttonPosition.X = 392;
                this.buttonPosition.Y += 50;
                PMainMenuItems.Add(new MenuItems(this.buttons[2], this.buttonPosition, "EXIT GAME", newFont, false));
            }

            PMainMenuItems[this.selectedEntry].Selected = true;
            foreach (var item in PMainMenuItems)
            {
                item.Draw(spriteBatch);
            }

            this.DrawCursor(spriteBatch);
            
            spriteBatch.End();
        }
        
        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            this.UpdateCursor();

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
                if (PMainMenuItems[this.selectedEntry].ItemText == "NEW GAME")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.ChooseHeroWindow);
                }

                if (PMainMenuItems[this.selectedEntry].ItemText == "RESUME GAME")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                }

                if (PMainMenuItems[this.selectedEntry].ItemText == "CONTROLS")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.ControlWindow);
                }

                if (PMainMenuItems[this.selectedEntry].ItemText == "ABOUT")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.AboutWindow);
                }

                if (PMainMenuItems[this.selectedEntry].ItemText == "EXIT GAME")
                {
                    Environment.Exit(1);
                }
            }

            if (this.previousMouse.LeftButton == ButtonState.Released && this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in PMainMenuItems)
                {
                    if (this.mouse.X > item.ItemPosition.X && this.mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                        this.mouse.Y > item.ItemPosition.Y && this.mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)                       
                    {
                        if (item.ItemText == "NEW GAME")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.ChooseHeroWindow);
                            break;
                        }

                        if (item.ItemText == "RESUME GAME")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.GameWindow);
                            break;
                        }

                        if (item.ItemText == "CONTROLS")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.ControlWindow);
                            break;
                        }

                        if (item.ItemText == "ABOUT")
                        {
                            Rpg.ActiveWindowSet(EnumActiveWindow.AboutWindow);
                            break;
                        }

                        if (item.ItemText == "EXIT GAME")
                        {
                            Environment.Exit(1);
                        }
                    }
                }
            }

            this.previousKeyboard = this.keyboard;
            this.previousMouse = this.mouse;
        }

        private void LoadCursor(ContentManager content)
        {
            this.cursor.SpriteIndex = content.Load<Texture2D>(string.Format("{0}{1}", @"Textures\Objects\", "cursor"));
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