namespace Rpg.Screens
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    
    class ControlScreen
    {
        private readonly IList<MenuItems> controlScreenItems = new List<MenuItems>();
        private readonly IList<Texture2D> planketTexture = new List<Texture2D>();
        private readonly int selectedEntry = 0;
        
        private Texture2D controlScreenBackgroundTexture;
        private Vector2 controlScreenBackgroundPosition;
        private Texture2D keyboardTexture;
        private Vector2 keyboardPosition;
        private Texture2D mouseTexture;
        private Vector2 mousePosition;

        private Vector2 planketPosition;

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;    

        public void Load(ContentManager content)
        {
            this.controlScreenBackgroundTexture = content.Load<Texture2D>(@"Textures\GameScreens\MainMenu");
            this.planketTexture.Add(content.Load<Texture2D>(@"Textures\GameScreens\MainMenuPlank"));  
         
            this.keyboardTexture = content.Load<Texture2D>(@"Textures\GameScreens\keyboard");
            this.mouseTexture = content.Load<Texture2D>(@"Textures\GameScreens\mouse");
        }

        public void Draw(GraphicsDevice graphicDevice, SpriteBatch spriteBatch, ContentManager content)
        {
            graphicDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            
            spriteBatch.Begin();
            
            this.controlScreenBackgroundPosition = new Vector2(0, 0);
            spriteBatch.Draw(this.controlScreenBackgroundTexture, this.controlScreenBackgroundPosition, Color.White);

            if (this.controlScreenItems.Count < 1)
            {
                // Back planket and text;
                this.planketPosition = new Vector2(700, 600);
                this.controlScreenItems.Add(new MenuItems(this.planketTexture[0], this.planketPosition, "Back", newFont, false));
            }

            this.controlScreenItems[this.selectedEntry].Selected = true;
            foreach (var item in this.controlScreenItems)
            {
                item.Draw(spriteBatch);
            }

            this.keyboardPosition = new Vector2(100, 300);
            spriteBatch.Draw(this.keyboardTexture, this.keyboardPosition, Color.White);

            this.mousePosition = new Vector2(600, 300);
            spriteBatch.Draw(this.mouseTexture, this.mousePosition, Color.White);

            spriteBatch.End();
        }

        public void Update()
        {
            this.mouse = Mouse.GetState();
            this.keyboard = Keyboard.GetState();

            if (this.keyboard.IsKeyDown(Keys.Enter) && this.previousKeyboard.IsKeyUp(Keys.Enter) &&
                this.controlScreenItems.Count != 0)
            {
                if (this.controlScreenItems[this.selectedEntry].ItemText == "Back")
                {
                    Rpg.ActiveWindowSet(EnumActiveWindow.MainMenu);
                }
            }

            if (this.mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in this.controlScreenItems)
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