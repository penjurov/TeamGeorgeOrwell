namespace RPG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class RPG : Microsoft.Xna.Framework.Game
    {
        private static Camera Camera = new Camera();
        private static EnumActiveWindow ActiveWindow;

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

        public static EnumActiveWindow PActiveWindow
        {
            get
            {
                return ActiveWindow;
            }
            private set
            {
                ActiveWindow = value;
            }
        }

        public static void ActiveWindowSet(EnumActiveWindow input)
        {
            PActiveWindow = input;
        }

        public static Camera PCamera
        {
            get
            {
                return Camera;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            PActiveWindow = EnumActiveWindow.MainMenu;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.viewport = GraphicsDevice.Viewport;

            this.mainMenuScreen.Load(this.Content);
            this.gameScreen.Load(this.Content, this.viewport, Camera, graphics);

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
