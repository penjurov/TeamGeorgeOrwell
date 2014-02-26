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

        protected override void Initialize()
        {
            base.Initialize();
            PActiveWindow = EnumActiveWindow.MainMenu;
            this.IsMouseVisible = false;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            
            this.mainMenuScreen.LoadObjects(this.Content);
            
            this.chooseHero.LoadObjects(this.Content);

            this.aboutScreen.LoadObjects(this.Content);

            this.controlScreen.LoadObjects(this.Content);

            this.gameOver.LoadObjects(this.Content);

            this.win.LoadObjects(this.Content);
                       
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        { 
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.chooseHero.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.ControlWindow)
            {
                this.controlScreen.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.AboutWindow)
            {
                this.aboutScreen.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.GameOver)
            {
                this.gameOver.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.Win)
            {
                this.win.UpdateObjects(this.Content);
                this.mainThemeInstance.Play();
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.mainThemeInstance.Stop();

                if (!this.loaded)
                {
                    this.gameScreen.LoadObjects(this.Content); 
                    this.loaded = true;
                }

                this.gameScreen.UpdateObjects(this.Content);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (activeWindow == EnumActiveWindow.MainMenu)
            {
                this.mainMenuScreen.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameWindow)
            {
                this.gameScreen.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.ChooseHeroWindow)
            {
                this.chooseHero.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.AboutWindow)
            {
                this.aboutScreen.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.ControlWindow)
            {
                this.controlScreen.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.GameOver)
            {
                this.gameOver.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }

            if (activeWindow == EnumActiveWindow.Win)
            {
                this.win.DrawObjects(this.graphics.GraphicsDevice, this.spriteBatch, this.Content);
            }
        }
    }
}