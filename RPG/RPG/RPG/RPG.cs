namespace Rpg
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Screens;

    public class Rpg : Microsoft.Xna.Framework.Game
    {
        private static Camera camera = new Camera();
        private static EnumActiveWindow activeWindow;

        private readonly GraphicsDeviceManager graphics;
        private readonly MainMenuScreen mainMenuScreen = new MainMenuScreen();
        private readonly GameScreen gameScreen = new GameScreen();

        private SpriteBatch spriteBatch;
        private Viewport viewport;

        public Rpg()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 700;
            this.Content.RootDirectory = "Content";
        }

        public static EnumActiveWindow PActiveWindow
        {
            get
            {
                return activeWindow;
            }

            private set
            {
                activeWindow = value;
            }
        }

        public static Camera Camera
        {
            get
            {
                return camera;
            }
            private set
            {
                camera = value;
            }
        }

        public static void ActiveWindowSet(EnumActiveWindow input)
        {
            PActiveWindow = input;
        }

        protected override void Initialize()
        {
            base.Initialize();
            PActiveWindow = EnumActiveWindow.MainMenu;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.viewport = this.GraphicsDevice.Viewport;

            this.mainMenuScreen.Load(this.Content);
            this.gameScreen.Load(this.Content, this.viewport, Camera, this.graphics);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.IsMouseVisible = true;
                this.mainMenuScreen.Update();
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.IsMouseVisible = false;
                this.gameScreen.Update(Camera);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.gameScreen.Draw(this.graphics.GraphicsDevice, this.viewport, this.spriteBatch, this.Content, Camera);
            }

            base.Draw(gameTime);
        }
    }
}
