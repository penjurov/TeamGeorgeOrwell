namespace RPG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Storage;
    using Microsoft.Xna.Framework.GamerServices;
    using System.Collections.Generic;

    public class RPG : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Viewport viewport;
        private Random rand = new Random();
        public static Rectangle screen;
        public static Rectangle room;
        public static Camera camera = new Camera();
        // Main menu Items

        private Texture2D mainMenuBackgroundTexture;
        private Vector2 mainMenuBackgroundPosition;

        private List<Texture2D> planketTexture = new List<Texture2D>();
        private Vector2 planketPosition;

        private List<menuItems> mainMenuItems = new List<menuItems>();

        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private MouseState mouse;
        private MouseState previousMouse;

        private int selectedEntry = 0;
               
        // Game Window Items
        private Texture2D gameWindowTexture;
        private Vector2 gameWindowTexturePos;
        private Vector2 range = new Vector2(0, 0);
        private Heroes soldier;
        private Cursor cursor = new Cursor(new Vector2(0, 0));

        public static List<Obj> bullets = new List<Obj>();
             
        private enumActiveWindow activeWindow;

        public enumActiveWindow ActiveWindow
        {
            get;
            set;
        }

        public RPG()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            room = new Rectangle(0, 0, graphics.PreferredBackBufferWidth * 2, graphics.PreferredBackBufferHeight * 2);
            screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
            activeWindow = enumActiveWindow.MainMenu;  
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewport = GraphicsDevice.Viewport;

            MainMenuLoad();
            GameWindowLoad();          
                        
            camera.Position = soldier.Position;
            base.LoadContent();           
        }

        protected override void Update(GameTime gameTime)
        {           
            if (activeWindow == enumActiveWindow.MainMenu)
            {
                this.IsMouseVisible = true;
                MainMenuUpdate();
            }

            if (activeWindow == enumActiveWindow.GameWindow)
            {
                this.IsMouseVisible = false;
                GameWindowUpdate();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {           
            if (activeWindow == enumActiveWindow.MainMenu)
            {
                MainMenuDraw();
            }

            if(activeWindow == enumActiveWindow.GameWindow) 
            {
                GameWindowDraw();
            }
                
            base.Draw(gameTime);
        }

        static void Main()
        {
            using (RPG game = new RPG())
            {
                game.Run();
            }
        }

        private void MainMenuLoad()
        {
            mainMenuBackgroundTexture = this.Content.Load<Texture2D>(@"Textures\MainMenu\MainMenu");

            planketTexture.Add(this.Content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank"));
            planketTexture.Add(this.Content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank02"));
            planketTexture.Add(this.Content.Load<Texture2D>(@"Textures\MainMenu\MainMenuPlank03"));

        }

        private void MainMenuDraw()
        {
            Color color = Color.Black;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteFont newFont = this.Content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
            mainMenuBackgroundPosition = new Vector2(0, 0);
            
            
            spriteBatch.Begin();

            spriteBatch.Draw(mainMenuBackgroundTexture, mainMenuBackgroundPosition, Color.White);

            if (mainMenuItems.Count < 4)
            {
                // New game planket and text;
                planketPosition = new Vector2(700, 260);
                mainMenuItems.Add(new menuItems(planketTexture[2], planketPosition, "New game", newFont, false));

                // Control planket and text
                planketPosition.Y += 80;
                mainMenuItems.Add(new menuItems(planketTexture[1], planketPosition, "Controls", newFont, false));

                // About planket and text
                planketPosition.Y += 80;
                mainMenuItems.Add(new menuItems(planketTexture[2], planketPosition, "About", newFont, false));

                // Exit game planket and text
                planketPosition.Y += 80;
                mainMenuItems.Add(new menuItems(planketTexture[0], planketPosition, "Exit game", newFont, false));    
            }

            mainMenuItems[selectedEntry].Selected = true;
            foreach (var item in mainMenuItems)
            {
                if (true)
                {
                    item.Draw(spriteBatch);    
                }                        
            }
                   
            spriteBatch.End(); 
        }

        private void MainMenuUpdate()
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down) && previousKeyboard.IsKeyUp(Keys.Down))
            {
                mainMenuItems[selectedEntry].Selected = false;
                if (selectedEntry < mainMenuItems.Count - 1)
                {
                   selectedEntry++;   
                }               
                mainMenuItems[selectedEntry].Selected = true;
            }

            if (keyboard.IsKeyDown(Keys.Up) && previousKeyboard.IsKeyUp(Keys.Up))
            {
                mainMenuItems[selectedEntry].Selected = false;
                if (selectedEntry > 0)
                {
                    selectedEntry--;
                }
                mainMenuItems[selectedEntry].Selected = true;
            }

            if (keyboard.IsKeyDown(Keys.Enter) && previousKeyboard.IsKeyUp(Keys.Enter))
            {
                if (mainMenuItems[selectedEntry].ItemText == "New game" || mainMenuItems[selectedEntry].ItemText == "Resume game")
                {
                    activeWindow = enumActiveWindow.GameWindow;
                }
                if (mainMenuItems[selectedEntry].ItemText == "Exit game")
                {
                    Environment.Exit(1);
                }
            }


            if (mouse.LeftButton == ButtonState.Pressed)
            {
                foreach (var item in mainMenuItems)
                {
                    if (mouse.X > item.ItemPosition.X && mouse.X < item.ItemPosition.X + item.ItemTexture.Bounds.Width &&
                    mouse.Y > item.ItemPosition.Y && mouse.Y < item.ItemPosition.Y + item.ItemTexture.Bounds.Height)
                   {
                       if (item.ItemText == "New game" || item.ItemText == "Resume game")
                       {
                           activeWindow = enumActiveWindow.GameWindow;
                       }
                       if (item.ItemText == "Exit game")
                       {
                           Environment.Exit(1);
                       }
                   }
                }
            }

            previousKeyboard = keyboard;
        }

        private void GameWindowLoad()
        {
            gameWindowTexture = this.Content.Load<Texture2D>(@"Textures\GameScreens\GameScreen");            
            Vector2 characterPosition = new Vector2((gameWindowTexture.Width - viewport.Width) / 2, 0);

            soldier = new Heroes(characterPosition, 3);
            
            soldier.LoadContent(this.Content, "male");
            cursor.LoadContent(this.Content, "crosshair");
            soldier.Ammo = 32;

            Texture2D bulletTexture = this.Content.Load<Texture2D>(@"Textures\Objects\bullet");

            for (int i = 0; i < soldier.Ammo; i++)
            {
                Obj o = new Bullet(new Vector2(0, 0), bulletTexture);
                o.Alive = false;
                bullets.Add(o);
            }
        }

        private void GameWindowDraw()
        {
            gameWindowTexturePos = new Vector2((viewport.Width - gameWindowTexture.Width) / 2, 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform(graphics.GraphicsDevice));
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(gameWindowTexture, gameWindowTexturePos, Color.White);
                soldier.Draw(spriteBatch, viewport);

                SpriteFont font = Content.Load<SpriteFont>(@"Fonts/Comic Sans MS");
                Vector2 ammoPosition = new Vector2(10, 10);
                spriteBatch.DrawString(font,
                        "Ammo :  " + soldier.Ammo, ammoPosition, Color.White);

                foreach (var bullet in bullets)
                {
                    if (bullet.Alive)
                    {
                        bullet.Draw(spriteBatch, viewport);    
                    }
                       
                }

            spriteBatch.End();

            spriteBatch.Begin();        
                cursor.Draw(spriteBatch, viewport);
            spriteBatch.End();
        }

        private void GameWindowUpdate()
        {
            mouse = Mouse.GetState();
            previousMouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            soldier.Update();
            cursor.Update();

            foreach (var bullet in bullets)
            {
                bullet.Update();    
            }

            if (keyboard.IsKeyDown(Keys.Tab) && previousKeyboard.IsKeyUp(Keys.Tab))
            {
                mainMenuItems[0].ItemText = "Resume game";
                activeWindow = enumActiveWindow.MainMenu;
            }

            soldier.FiringTimer++;
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (soldier.Ammo > 0)
                {                    
                    soldier.CheckShooting();    
                }               
            }
          
            previousKeyboard = keyboard;
            previousMouse = mouse;

            camera.Position = soldier.Position;
        }


    }
}
