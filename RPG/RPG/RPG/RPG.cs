namespace RPG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class RPG : Microsoft.Xna.Framework.Game
    {
        public static Rectangle Screen;
        public static Rectangle Room;
        public static Camera Camera = new Camera();
        public static EnumActiveWindow ActiveWindow;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Viewport viewport;
                    
        MainMenuScreen mainMenuScreen = new MainMenuScreen();                    
        GameScreen gameScreen = new GameScreen();
                
        public RPG()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            Room = new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth * 2, this.graphics.PreferredBackBufferHeight * 2);
            Screen = new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight);
            base.Initialize();
            ActiveWindow = EnumActiveWindow.MainMenu;  
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.viewport = GraphicsDevice.Viewport;

            this.mainMenuScreen.Load(this.Content);
            this.gameScreen.Load(this.Content, this.viewport, Camera);            
            
            base.LoadContent();           
        }

        protected override void Update(GameTime gameTime)
        {           
            if (ActiveWindow == EnumActiveWindow.MainMenu)
            {
                this.IsMouseVisible = true;
                this.mainMenuScreen.Update();
            }

            if (ActiveWindow == EnumActiveWindow.GameWindow)
            {
                this.IsMouseVisible = false;
                this.gameScreen.Update(Camera);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {           
            if (ActiveWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (ActiveWindow == EnumActiveWindow.GameWindow)
            {
                this.gameScreen.Draw(this.graphics.GraphicsDevice, this.viewport, this.spriteBatch, this.Content, Camera);
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
    }
}
