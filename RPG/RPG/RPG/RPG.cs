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
        private readonly AboutScreen aboutScreen = new AboutScreen();
        private readonly ControlScreen controlScreen = new ControlScreen();
        private readonly GameOver gameOver = new GameOver();
        private readonly WinScreen win = new WinScreen();
        private readonly SoundEffect mainTheme;
        private readonly SoundEffectInstance mainThemeInstance;

        private SpriteBatch spriteBatch;        

        private bool loaded = false;

        public Rpg()
        {
            this.graphics = new GraphicsDeviceManager(this);

            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 715;

            this.graphics.IsFullScreen = false;

            this.Content.RootDirectory = "Content";

            this.mainTheme = this.Content.Load<SoundEffect>(@"Textures\Sounds\mainTheme");
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

            set
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
            this.IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            
            this.mainMenuScreen.Load(this.Content);
            
            this.chooseHero.Load(this.Content);

            this.aboutScreen.Load(this.Content);

            this.controlScreen.Load(this.Content);

            this.gameOver.Load(this.Content);

            this.win.Load(this.Content);
                       
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        { 
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.chooseHero.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.ControlWindow)
            {
                this.controlScreen.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.AboutWindow)
            {
                this.aboutScreen.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.GameOver)
            {
                this.gameOver.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.Win)
            {
                this.win.Update();
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                if (!this.loaded)
                {
                    this.gameScreen.Load(this.Content); 
                    this.loaded = true;
                }

                this.gameScreen.Update(this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.mainThemeInstance.Stop();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.gameScreen.Draw(this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.chooseHero.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.AboutWindow)
            {
                this.aboutScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.ControlWindow)
            {
                this.controlScreen.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameOver)
            {
                this.gameOver.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.Win)
            {
                this.win.Draw(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }
        }
    }
}