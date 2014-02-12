namespace Rpg
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Screens;

    public class Rpg : Game
    {
        private static EnumActiveWindow activeWindow;

        private readonly GraphicsDeviceManager graphics;
        private readonly MainMenuScreen mainMenuScreen = new MainMenuScreen();
        private readonly GameScreen gameScreen = new GameScreen();
        private readonly ChooseHeroScreen chooseHero = new ChooseHeroScreen();

        private SpriteBatch spriteBatch;
        private Viewport viewport;

        private SoundEffect mainTheme;
        private SoundEffectInstance mainThemeInstance;

        private bool loaded = false;

        public Rpg()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1000;
            this.graphics.PreferredBackBufferHeight = 700;
            this.graphics.IsFullScreen = false;
            this.Content.RootDirectory = "Content";

            this.mainTheme = Content.Load<SoundEffect>(@"Textures\Sounds\mainTheme");
            this.mainThemeInstance = this.mainTheme.CreateInstance();
            this.mainThemeInstance.IsLooped = false;
            this.mainThemeInstance.Volume = 0.2f;
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
            
            this.chooseHero.Load(this.Content);
                       
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.IsMouseVisible = true;
                this.mainMenuScreen.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.IsMouseVisible = true;
                this.chooseHero.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                if (!loaded)
                {
                    this.gameScreen.Load(this.Content, this.viewport, this.graphics); 
                    loaded = true;
                }

                this.IsMouseVisible = false;
                this.gameScreen.Update(viewport);
            }

            if (activeWindow != EnumActiveWindow.MainMenu)
            {
                this.mainThemeInstance.Stop();
            }
         //   base.Update(gameTime);           
        }

        protected override void Draw(GameTime gameTime)
        {

            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.gameScreen.Draw(this.graphics.GraphicsDevice, this.viewport, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.chooseHero.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

           // base.Draw(gameTime);
        }
    }
}
